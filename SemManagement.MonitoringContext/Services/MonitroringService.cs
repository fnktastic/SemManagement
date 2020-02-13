﻿using SemManagement.MonitoringContext.Enum;
using SemManagement.MonitoringContext.Model;
using SemManagement.MonitoringContext.Repository;
using SemManagement.MonitoringContext.Scheduler;
using SemManagement.MonitoringContext.Scheduler.Jobs;
using SemManagement.SemContext.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Services
{
    public interface IMonitoringService
    {
        Task MonitorActiveStations();
        Task MonitorPlaylists();
        Task<BoolResult> ColdStartMonitoring();
    }

    public class MonitoringService : IMonitoringService
    {
        private readonly IPlaylistRepository _semPlaylistRepository;
        private readonly ISongRepository _semSongRepository;
        private readonly IStationRepository _semStationRepository;
        private readonly IRuleService _ruleService;
        private readonly ISnapshotEntryRepository _snapshotEntryRepository;
        private readonly IMonitoringRepositry _monitoringRepositry;

        public MonitoringService(ISnapshotEntryRepository snapshotEntryRepository, IRuleService ruleService, IStationRepository semStationRepository, IMonitoringRepositry monitoringRepositry, IPlaylistRepository semPlaylistRepository, ISongRepository semSongRepository)//, MonitoringScheduler monitoringScheduler)
        {
            _semStationRepository = semStationRepository;
            _monitoringRepositry = monitoringRepositry;
            _semPlaylistRepository = semPlaylistRepository;
            _semSongRepository = semSongRepository;
            _ruleService = ruleService;
            _snapshotEntryRepository = snapshotEntryRepository;
        }

        public async Task MonitorPlaylists()
        {
            var snapshotEntry = await _snapshotEntryRepository.GetLast(MonitorTypeEnum.Playlists, MonitorStateEnum.Finished);

            var now = DateTime.Now;

            var startEntry = new MonitoringDto(MonitorTypeEnum.Playlists, MonitorStateEnum.Started, now);

            await FullPlaylistSnapshot(now);

            await LightPlaylistSnapshot(snapshotEntry, now);

            await _snapshotEntryRepository.InsertAsync(startEntry);

            await _snapshotEntryRepository.InsertAsync(new MonitoringDto(MonitorTypeEnum.Playlists, MonitorStateEnum.Finished, DateTime.Now));
        }

        public async Task MonitorActiveStations()
        {
            var snapshotEntry = await _snapshotEntryRepository.GetLast(MonitorTypeEnum.Stations, MonitorStateEnum.Finished);

            DateTime now = DateTime.Now;

            var startEntry = new MonitoringDto(MonitorTypeEnum.Stations, MonitorStateEnum.Started, now);

            var activeStations = await _monitoringRepositry.GetMonitoredStations();

            foreach (var station in activeStations)
            {
                var stationSnapshots = new List<StationSnapshotDto>();

                var stationSnapshotPlaylists = new List<StationSnapshotPlaylistDto>();

                var stationSnapshot = new StationSnapshotDto()
                {
                    Id = Guid.NewGuid(),
                    DateTime = now,
                    StationId = station.StationId,
                    StationMonitoringId = station.Id
                };

                var stationPlaylists = (await _semPlaylistRepository.GetModifiedPlaylistsByStationAsync(station.StationId, snapshotEntry.Timestamp))
                    .Select(x => new StationSnapshotPlaylistDto()
                    {
                        DateTime = now,
                        PlaylistId = x.Plid,
                        StationSnapshotId = stationSnapshot.Id,
                    }).ToList();

                stationSnapshots.Add(stationSnapshot);

                stationSnapshotPlaylists.AddRange(stationPlaylists);

                await _monitoringRepositry.SaveStationSnapshots(stationSnapshots);

                await _monitoringRepositry.SaveStationSnapshotPlaylists(stationSnapshotPlaylists);
            }

            await _snapshotEntryRepository.InsertAsync(startEntry);

            await _snapshotEntryRepository.InsertAsync(new MonitoringDto(MonitorTypeEnum.Stations, MonitorStateEnum.Finished, DateTime.Now));
        }

        public async Task<BoolResult> ColdStartMonitoring()
        {
            try
            {
                await MonitorActiveStations();

                await MonitorPlaylists();

                //await _ruleService.FireRules();

                return new BoolResult(true);
            }
            catch
            {
                return new BoolResult(true);
            }
        }


        #region private methods
        private async Task AddStationPlaylistsToMonitoring(List<StationMonitoringDto> stationMonitorings)
        {
            foreach (var stationMonitoring in stationMonitorings)
            {
                var stationPlaylists = await _semPlaylistRepository.GetPlaylistsByStationAsync(stationMonitoring.StationId);

                if (stationPlaylists != null && stationPlaylists.Count > 0)
                {
                    var playlistMonitoring = stationPlaylists.Select(x => new PlaylistMonitoringDto()
                    {
                        Id = $"{x.Plid}-{stationMonitoring.StationId}",
                        PlaylistId = x.Plid,
                        PlaylistName = x.Name,
                        TargetStationId = stationMonitoring.StationId
                    }).ToList();

                    await _monitoringRepositry.SavePlaylistsMonitrorings(playlistMonitoring);
                }
            }
        }

        private async Task FullPlaylistSnapshot(DateTime now) //for monitored playlists, different  alculation 
        {
            var activeStations = await _monitoringRepositry.GetMonitoredStations();

            await AddStationPlaylistsToMonitoring(activeStations);

            var playlistSnapshots = new List<PlaylistSnapshotDto>();

            var playlistSnapshotSongs = new List<PlaylistSnapshotSongDto>();

            var monitoredPlaylists = (await _monitoringRepositry.GetMonitoredPlaylists())
                .Select(x => x.PlaylistId)
                .Distinct()
                .ToList();

            foreach (var monitoredPlaylist in monitoredPlaylists)
            {
                var playlist = await _semPlaylistRepository.GetPlaylistById(monitoredPlaylist);

                var playlistSnapshot = new PlaylistSnapshotDto()
                {
                    Id = Guid.NewGuid(),
                    DateTime = now,
                    PlaylistId = playlist.Plid,
                    PlaylistName = playlist.Name
                };

                var playlistSongs = (await _semSongRepository.GetSongsByPlaylistAsync(playlist.Plid))
                    .Select(x => new PlaylistSnapshotSongDto()
                    {
                        DateTime = now,
                        PlaylistSnapshotId = playlistSnapshot.Id,
                        SongId = x.Sgid
                    }).ToList();

                playlistSnapshots.Add(playlistSnapshot);

                playlistSnapshotSongs.AddRange(playlistSongs);
            }

            await _monitoringRepositry.SavePlaylistSnapshots(playlistSnapshots);

            await _monitoringRepositry.SavePlaylistSnapshotSongs(playlistSnapshotSongs);

        }

        private async Task LightPlaylistSnapshot(MonitoringDto snapshotEntry, DateTime now) //only to see new data per playlist
        {
            var playlistSnapshots = new List<PlaylistSnapshotDto>();

            var playlistSnapshotSongs = new List<PlaylistSnapshotSongDto>();

            var modifiedPlaylistsIds = (await _semPlaylistRepository.GetModifiedPlaylistsSongs(snapshotEntry.Timestamp))
                .Select(x => x.Plid)
                .Distinct()
                .ToList();

            foreach (var modifiedPlaylistsId in modifiedPlaylistsIds)
            {
                var playlist = await _semPlaylistRepository.GetPlaylistById(modifiedPlaylistsId);

                var playlistSnapshot = new PlaylistSnapshotDto()
                {
                    Id = Guid.NewGuid(),
                    DateTime = now,
                    PlaylistId = playlist.Plid,
                    PlaylistName = playlist.Name
                };

                var playlistSongs = (await _semSongRepository.GetSongsByPlaylistAsync(playlist.Plid, snapshotEntry.Timestamp))
                    .Select(x => new PlaylistSnapshotSongDto()
                    {
                        DateTime = now,
                        PlaylistSnapshotId = playlistSnapshot.Id,
                        SongId = x.Sgid
                    }).ToList();

                playlistSnapshots.Add(playlistSnapshot);

                playlistSnapshotSongs.AddRange(playlistSongs);
            }

            await _monitoringRepositry.SavePlaylistSnapshots(playlistSnapshots);

            await _monitoringRepositry.SavePlaylistSnapshotSongs(playlistSnapshotSongs);
        }
        #endregion
    }
}
