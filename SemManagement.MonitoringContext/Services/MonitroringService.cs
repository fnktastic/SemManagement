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
        Task AddMonitoringStation(StationMonitoring stationMonitoring);
    }

    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringRepositry _monitoringRepositry;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ISongRepository _songRepository;

        public MonitoringService(IMonitoringRepositry monitoringRepositry, IPlaylistRepository playlistRepository, ISongRepository songRepository)//, MonitoringScheduler monitoringScheduler)
        {
            _monitoringRepositry = monitoringRepositry;
            _playlistRepository = playlistRepository;
            _songRepository = songRepository;
        }

        public async Task AddMonitoringStation(StationMonitoring stationMonitoring)
        {
            await _monitoringRepositry.AddMonitoringStation(stationMonitoring);
        }

        public async Task MonitorStations()
        {
            var monitoredStations = await _monitoringRepositry.GetMonitoredStations();

            DateTime now = DateTime.UtcNow;

            var stationSnapshots = new List<StationSnapshot>();

            var stationSnapshotPlaylists = new List<StationSnapshotPlaylist>();

            var monitoredPlaylistsIds = new List<int>();

            foreach (var monitoredStation in monitoredStations)
            {
                var stationSnapshot = new StationSnapshot()
                {
                    Id = Guid.NewGuid(),
                    DateTime = now,
                    StationId = monitoredStation.StationId,
                    StationMonitoringId = monitoredStation.Id
                };

                var stationPlaylists = (await _playlistRepository.GetPlaylistsByStationAsync(monitoredStation.StationId))
                    .Select(x => new StationSnapshotPlaylist()
                    {
                        DateTime = now,
                        PlaylistId = x.Plid,
                        StationSnapshotId = stationSnapshot.Id,
                    }).ToList(); ;

                stationSnapshots.Add(stationSnapshot);

                stationSnapshotPlaylists.AddRange(stationPlaylists);

                monitoredPlaylistsIds.AddRange(stationPlaylists.Select(x => x.PlaylistId));
            }

            await _monitoringRepositry.SaveStationSnapshots(stationSnapshots);

            await _monitoringRepositry.SaveStationSnapshotPlaylists(stationSnapshotPlaylists);

            //---

            monitoredPlaylistsIds = monitoredPlaylistsIds.Distinct().ToList();

            var playlistSnapshots = new List<PlaylistSnapshot>();

            var playlistSnapshotSongs = new List<PlaylistSnapshotSong>();

            foreach (var monitoredPlaylistsId in monitoredPlaylistsIds)
            {
                var playlistSnapshot = new PlaylistSnapshot()
                {
                    Id = Guid.NewGuid(),
                    DateTime = now,
                    PlaylistId = monitoredPlaylistsId
                };

                var playlistSongs = (await _songRepository.GetSongsByPlaylistAsync(monitoredPlaylistsId))
                    .Select(x => new PlaylistSnapshotSong()
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
    }
}
