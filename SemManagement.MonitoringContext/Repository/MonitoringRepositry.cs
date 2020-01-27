using Microsoft.EntityFrameworkCore;
using SemManagement.MonitoringContext.DataAccess;
using SemManagement.MonitoringContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Repository
{
    public interface IMonitoringRepositry
    {
        Task<List<StationMonitoring>> GetMonitoredStations();

        Task AddMonitoringStation(StationMonitoring stationMonitoring);

        Task SaveStationSnapshots(List<StationSnapshot> stationSnapshots);

        Task SaveStationSnapshotPlaylists(List<StationSnapshotPlaylist> stationSnapshotPlaylists);

        Task SavePlaylistSnapshots(List<PlaylistSnapshot> playlistSnapshots);

        Task SavePlaylistSnapshotSongs(List<PlaylistSnapshotSong> playlistSnapshots);

        Task<bool> CheckIfExist(StationMonitoring stationMonitoring);
    }

    public class MonitoringRepositry : IMonitoringRepositry
    {
        private readonly MonitoringDbContext _context;

        public MonitoringRepositry(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task AddMonitoringStation(StationMonitoring stationMonitoring)
        {
            _context.StationMonitorings.Add(stationMonitoring);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfExist(StationMonitoring stationMonitoring)
        {
            var items = await _context.StationMonitorings.Where(x => x.StationId == stationMonitoring.StationId).ToListAsync();

            if (items.Count > 0) return true;

            return false;
        }

        public async Task<List<StationMonitoring>> GetMonitoredStations()
        {
            return await _context.StationMonitorings.ToListAsync();
        }

        public async Task SavePlaylistSnapshots(List<PlaylistSnapshot> playlistSnapshots)
        {
            _context.PlaylistSnapshots.AddRange(playlistSnapshots);

            await _context.SaveChangesAsync();
        }

        public async Task SavePlaylistSnapshotSongs(List<PlaylistSnapshotSong> playlistSnapshots)
        {
            _context.PlaylistSnapshotSongs.AddRange(playlistSnapshots);

            await _context.SaveChangesAsync();
        }

        public async Task SaveStationSnapshotPlaylists(List<StationSnapshotPlaylist> stationSnapshotPlaylists)
        {
            _context.StationSnapshotPlaylists.AddRange(stationSnapshotPlaylists);

            await _context.SaveChangesAsync();
        }

        public async Task SaveStationSnapshots(List<StationSnapshot> stationSnapshots)
        {
            _context.StationSnapshots.AddRange(stationSnapshots);

            await _context.SaveChangesAsync();
        }
    }
}
