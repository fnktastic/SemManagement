using SemManagement.MonitoringContext.Enum;
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
        Task MonitorAllActiveStations();
        Task MonitorPlaylists();
        Task ColdStartMonitoring();
    }

    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringRepositry _monitoringRepositry;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ISongRepository _songRepository;
        private readonly IStationRepository _stationRepository;
        private readonly IRuleService _ruleService;
        private readonly ISnapshotEntryRepository _snapshotEntryRepository;

        public MonitoringService(ISnapshotEntryRepository snapshotEntryRepository, IRuleService ruleService, IStationRepository stationRepository, IMonitoringRepositry monitoringRepositry, IPlaylistRepository playlistRepository, ISongRepository songRepository)//, MonitoringScheduler monitoringScheduler)
        {
            _stationRepository = stationRepository;
            _monitoringRepositry = monitoringRepositry;
            _playlistRepository = playlistRepository;
            _songRepository = songRepository;
            _ruleService = ruleService;
            _snapshotEntryRepository = snapshotEntryRepository;
        }

        public async Task MonitorPlaylists()
        {
            var snapshotEntry = await _snapshotEntryRepository.GetLast(MonitorTypeEnum.Playlists, MonitorStateEnum.Started);

            var now = DateTime.Now;

            await _snapshotEntryRepository.InsertAsync(new MonitoringDto(MonitorTypeEnum.Playlists, MonitorStateEnum.Started, now));

            var playlists = await _playlistRepository.TakeAsync(int.MaxValue, lastSnapshotAt: snapshotEntry.Timestamp);

            var playlistSnapshots = new List<PlaylistSnapshotDto>();

            var playlistSnapshotSongs = new List<PlaylistSnapshotSongDto>();

            var modifiedPlaylistsIds = (await _playlistRepository.GetModifiedPlaylistsSongs(snapshotEntry.Timestamp)).Select(x => x.Plid).Distinct().ToList();

            foreach (var modifiedPlaylistsId in modifiedPlaylistsIds)
            {
                var playlist = await _playlistRepository.GetPlaylistById(modifiedPlaylistsId);

                var playlistSnapshot = new PlaylistSnapshotDto()
                {
                    Id = Guid.NewGuid(),
                    DateTime = now,
                    PlaylistId = playlist.Plid,
                    PlaylistName = playlist.Name
                };

                var playlistSongs = (await _songRepository.GetSongsByPlaylistAsync(playlist.Plid, snapshotEntry.Timestamp))
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

            await _snapshotEntryRepository.InsertAsync(new MonitoringDto(MonitorTypeEnum.Playlists, MonitorStateEnum.Finished, DateTime.Now));
        }

        public async Task MonitorAllActiveStations()
        {
            var snapshotEntry = await _snapshotEntryRepository.GetLast(MonitorTypeEnum.Stations, MonitorStateEnum.Started);

            DateTime now = DateTime.Now;

            await _snapshotEntryRepository.InsertAsync(new MonitoringDto(MonitorTypeEnum.Stations, MonitorStateEnum.Started, now));

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

                var stationPlaylists = (await _playlistRepository.GetPlaylistsByStationAsync(station.StationId, snapshotEntry.Timestamp))
                    .Select(x => new StationSnapshotPlaylistDto()
                    {
                        DateTime = now,
                        PlaylistId = x.Plid,
                        StationSnapshotId = stationSnapshot.Id,
                    }).ToList(); ;

                stationSnapshots.Add(stationSnapshot);

                stationSnapshotPlaylists.AddRange(stationPlaylists);

                await _monitoringRepositry.SaveStationSnapshots(stationSnapshots);

                await _monitoringRepositry.SaveStationSnapshotPlaylists(stationSnapshotPlaylists);
            }

            await _snapshotEntryRepository.InsertAsync(new MonitoringDto(MonitorTypeEnum.Stations, MonitorStateEnum.Finished, DateTime.Now));
        }

        //private Task FullPlaylistSnapshot(int plid, DateTime now) //for monitored playlists, different  alculation 
        //{

        //}

        //private Task LightPlaylistSnapshot(int plid, DateTime now) //only to see new data per playlist
        //{

        //}

        //private Task FullStationSnapshot(int sid, DateTime now)
        //{

        //}

        //private Task LightStationSnapshot(int sid, DateTime now)
        //{

        //}

        public async Task ColdStartMonitoring()
        {
            await MonitorAllActiveStations();

            await MonitorPlaylists();

            await _ruleService.FireRules();
        }
    }
}
