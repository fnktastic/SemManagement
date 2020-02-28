using SemManagement.MonitoringContext.BusinessLogic;
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
        Task MonitorStations();
        Task MonitorPlaylists();
        Task<BoolResult> ColdStartMonitoring();
        Task<FeedList> GetQucikMonitoringForStaton(int sid);
    }

    public class MonitoringService : IMonitoringService
    {
        private readonly IPlaylistRepository _semPlaylistRepository;
        private readonly ISongRepository _semSongRepository;
        private readonly IStationRepository _semStationRepository;
        private readonly IRuleService _ruleService;
        private readonly ISnapshotEntryRepository _snapshotEntryRepository;
        private readonly IMonitoringRepositry _monitoringRepositry;
        private readonly IFeedRepository _feedRepository;

        public MonitoringService(IFeedRepository feedRepository, ISnapshotEntryRepository snapshotEntryRepository, IRuleService ruleService, IStationRepository semStationRepository, IMonitoringRepositry monitoringRepositry, IPlaylistRepository semPlaylistRepository, ISongRepository semSongRepository)//, MonitoringScheduler monitoringScheduler)
        {
            _semStationRepository = semStationRepository;
            _monitoringRepositry = monitoringRepositry;
            _semPlaylistRepository = semPlaylistRepository;
            _semSongRepository = semSongRepository;
            _ruleService = ruleService;
            _snapshotEntryRepository = snapshotEntryRepository;
            _feedRepository = feedRepository;
        }

        public async Task MonitorPlaylists()
        {
            var snapshotEntry = await _snapshotEntryRepository.GetLast(MonitorTypeEnum.Playlists, MonitorStateEnum.Finished);

            var now = DateTime.Now;

            var startEntry = new MonitoringDto(MonitorTypeEnum.Playlists, MonitorStateEnum.Started, now);

            //await FullPlaylistSnapshot(now);

            await LightPlaylistSnapshot(snapshotEntry, now);

            await _snapshotEntryRepository.InsertAsync(startEntry);

            await _snapshotEntryRepository.InsertAsync(new MonitoringDto(MonitorTypeEnum.Playlists, MonitorStateEnum.Finished, DateTime.Now));
        }

        public async Task<BoolResult> ColdStartMonitoring()
        {
            try
            {
                await MonitorStations();

                await MonitorPlaylists();

                //await _ruleService.FireRules();

                return new BoolResult(true);
            }
            catch
            {
                return new BoolResult(false);
            }
        }

        public async Task MonitorStations()
        {
            await MonitorActiveStations();

            await MonitorStationPlayerSate();
        }

        #region private methods
        private async Task MonitorActiveStations()
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
                    StationMonitoringId = station.Id,
                };

                var stationPlaylists = (await _semPlaylistRepository.GetModifiedPlaylistsByStationAsync(station.StationId, snapshotEntry.Timestamp))
                    .Select(x => new StationSnapshotPlaylistDto()
                    {
                        DateTime = x.Last_Update_Date,
                        PlaylistId = x.Plid,
                        PlaylistName = x.Name,
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

        #region under review
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
                    DateTime = playlist.Last_Update_Date,
                    PlaylistId = playlist.Plid,
                    PlaylistName = playlist.Name
                };

                var playlistSongs = (await _semSongRepository.GetSongsByPlaylistAsync(playlist.Plid))
                    .Select(x => new PlaylistSnapshotSongDto()
                    {
                        DateTime = x.Last_Update_Date,
                        PlaylistSnapshotId = playlistSnapshot.Id,
                        SongId = x.Sgid,
                        SongName = $"{x.Artist} - {x.Title}",
                        SnapshotAction = SnapshotActionEnum.Add
                    }).ToList();

                playlistSnapshots.Add(playlistSnapshot);

                playlistSnapshotSongs.AddRange(playlistSongs);
            }

            await _monitoringRepositry.SavePlaylistSnapshots(playlistSnapshots);

            await _monitoringRepositry.SavePlaylistSnapshotSongs(playlistSnapshotSongs);

        }
        #endregion

        private async Task LightPlaylistSnapshot(MonitoringDto snapshotEntry, DateTime now) //only to see new data per playlist
        {
            var playlistSnapshots = new List<PlaylistSnapshotDto>();

            var playlistSnapshotSongs = new List<PlaylistSnapshotSongDto>();

            var modifiedPlaylistsIds = (await _semPlaylistRepository.GetModifiedPlaylists(snapshotEntry.Timestamp))
                .Select(x => x.Plid);

            foreach (var modifiedPlaylistsId in modifiedPlaylistsIds)
            {
                var playlist = await _semPlaylistRepository.GetPlaylistById(modifiedPlaylistsId);

                var playlistSnapshot = new PlaylistSnapshotDto()
                {
                    Id = Guid.NewGuid(),
                    DateTime = playlist.Last_Update_Date,
                    PlaylistId = playlist.Plid,
                    PlaylistName = playlist.Name,
                    SnapshotAction = SnapshotActionEnum.Add
                };

                playlistSnapshots.Add(playlistSnapshot);
            }

            var modifiedPlaylistsSongsIds = (await _semPlaylistRepository.GetModifiedPlaylistsSongs(snapshotEntry.Timestamp))
                .Select(x => x.Plid)
                .Distinct();

            foreach (var modifiedPlaylistsSongsId in modifiedPlaylistsSongsIds)
            {
                var playlist = await _semPlaylistRepository.GetPlaylistById(modifiedPlaylistsSongsId);

                var playlistSnapshot = new PlaylistSnapshotDto()
                {
                    Id = Guid.NewGuid(),
                    DateTime = playlist.Last_Update_Date,
                    PlaylistId = playlist.Plid,
                    PlaylistName = playlist.Name,
                    SnapshotAction = SnapshotActionEnum.None
                };

                var playlistSongs = (await _semSongRepository.GetModifiedSongsByPlaylistAsync(playlist.Plid, snapshotEntry.Timestamp))
                    .Select(x => new PlaylistSnapshotSongDto()
                    {
                        DateTime = x.Last_Update_Date,
                        PlaylistSnapshotId = playlistSnapshot.Id,
                        SongName = $"{x.Artist} - {x.Title}",
                        SongId = x.Sgid,
                        SnapshotAction = SnapshotActionEnum.Add
                    }).ToList();

                playlistSnapshots.Add(playlistSnapshot);

                playlistSnapshotSongs.AddRange(playlistSongs);
            }

            await _monitoringRepositry.SavePlaylistSnapshots(playlistSnapshots);

            await _monitoringRepositry.SavePlaylistSnapshotSongs(playlistSnapshotSongs);
        }

        private async Task MonitorStationPlayerSate()
        {
            var now = DateTime.Now;

            var startEntry = new MonitoringDto(MonitorTypeEnum.PlayerState, MonitorStateEnum.Started, now);

            var activeStations = await _monitoringRepositry.GetMonitoredStations();

            var stationPlayerState = new List<StationPlayerStateDto>();

            foreach (var activeStation in activeStations)
            {
                var stationStatus = await _semStationRepository.GetStationStatuses(activeStation.StationId);

                var stationPlayerStateDto = new StationPlayerStateDto()
                {
                    Id = Guid.NewGuid(),
                    ChangedDate = stationStatus.ChangedDate,
                    DateTime = DateTime.Now,
                    StationId = activeStation.StationId,
                    CrossFade = stationStatus.CrossFade,
                    CurrentSongArtist = stationStatus.CurrentSongArtist,
                    CurrentSongSemId = stationStatus.CurrentSongSemId,
                    CurrentSongTitle = stationStatus.CurrentSongTitle,
                    Loop = stationStatus.Loop,
                    Mute = stationStatus.Mute,
                    Online = stationStatus.Online,
                    Playing = stationStatus.Playing,
                    QueueName = stationStatus.QueueName,
                    schedulerenabled = stationStatus.schedulerenabled,
                    Shuffle = stationStatus.Shuffle,
                    Stopped = stationStatus.Stopped,
                    Volume = stationStatus.Volume
                };

                stationPlayerState.Add(stationPlayerStateDto);
            }

            await _monitoringRepositry.SaveStationPlayerStateRangeAsync(stationPlayerState);

            await _snapshotEntryRepository.InsertAsync(startEntry);

            await _snapshotEntryRepository.InsertAsync(new MonitoringDto(MonitorTypeEnum.PlayerState, MonitorStateEnum.Finished, DateTime.Now));

        }

        public async Task<FeedList> GetQucikMonitoringForStaton(int sid)
        {
            var plids = (await _semPlaylistRepository.GetPlaylistsByStationAsync(sid)).Select(x => x.Plid).ToList();

            return await _monitoringRepositry.GetQucikMonitoringForStaton(plids, sid);
        }
        #endregion
    }
}
