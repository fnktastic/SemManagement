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
    }

    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringRepositry _monitoringRepositry;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ISongRepository _songRepository;
        private readonly IStationRepository _stationRepository;

        public MonitoringService(IStationRepository stationRepository, IMonitoringRepositry monitoringRepositry, IPlaylistRepository playlistRepository, ISongRepository songRepository)//, MonitoringScheduler monitoringScheduler)
        {
            _stationRepository = stationRepository;
            _monitoringRepositry = monitoringRepositry;
            _playlistRepository = playlistRepository;
            _songRepository = songRepository;
        }

        public async Task MonitorPlaylists()
        {
            var now = DateTime.UtcNow;

            var playlists = await _playlistRepository.TakeAsync(int.MaxValue);

            var playlistSnapshots = new List<PlaylistSnapshotDto>();

            var playlistSnapshotSongs = new List<PlaylistSnapshotSongDto>();

            foreach (var playlist in playlists)
            {
                var playlistSnapshot = new PlaylistSnapshotDto()
                {
                    Id = Guid.NewGuid(),
                    DateTime = now,
                    PlaylistId = playlist.Plid,
                    PlaylistName = playlist.Name
                };

                var playlistSongs = (await _songRepository.GetSongsByPlaylistAsync(playlist.Plid))
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

        public async Task MonitorAllActiveStations()
        {
            DateTime now = DateTime.UtcNow;

            var activeStations = await _monitoringRepositry.GetMonitoredStations();

            foreach (var station in activeStations)
            {
                await MonitorStation(station, now);
            }
        }

        private async Task MonitorStation(StationMonitoringDto monitoredStation, DateTime? dateTime)
        {
            if (monitoredStation == null) return;

            DateTime now = DateTime.UtcNow;

            if (dateTime.HasValue == true)
                now = dateTime.Value;

            var stationSnapshots = new List<StationSnapshotDto>();

            var stationSnapshotPlaylists = new List<StationSnapshotPlaylistDto>();

            var stationSnapshot = new StationSnapshotDto()
            {
                Id = Guid.NewGuid(),
                DateTime = now,
                StationId = monitoredStation.StationId,
                StationMonitoringId = monitoredStation.Id
            };

            var stationPlaylists = (await _playlistRepository.GetPlaylistsByStationAsync(monitoredStation.StationId))
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
    }
}
