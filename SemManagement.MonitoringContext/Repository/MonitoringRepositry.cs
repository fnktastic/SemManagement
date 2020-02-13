using EFCore.BulkExtensions;
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
        Task<List<StationMonitoringDto>> GetMonitoredStations();

        Task AddMonitoringStation(StationMonitoringDto stationMonitoring);

        Task SaveStationSnapshots(List<StationSnapshotDto> stationSnapshots);

        Task SaveStationSnapshotPlaylists(List<StationSnapshotPlaylistDto> stationSnapshotPlaylists);

        Task SavePlaylistSnapshots(List<PlaylistSnapshotDto> playlistSnapshots);

        Task SavePlaylistSnapshotSongs(List<PlaylistSnapshotSongDto> playlistSnapshots);

        Task<bool> CheckIfExist(StationMonitoringDto stationMonitoring);

        Task<List<PlaylistMonitoringDto>> GetMonitoredPlaylists();

        Task SavePlaylistsMonitrorings(List<PlaylistMonitoringDto> playlistMonitorings);
    }

    public class MonitoringRepositry : IMonitoringRepositry
    {
        private readonly MonitoringDbContext _context;

        public MonitoringRepositry(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task AddMonitoringStation(StationMonitoringDto stationMonitoring)
        {
            _context.StationMonitorings.Add(stationMonitoring);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfExist(StationMonitoringDto stationMonitoring)
        {
            var items = await _context.StationMonitorings.Where(x => x.StationId == stationMonitoring.StationId).ToListAsync();

            if (items.Count > 0) return true;

            return false;
        }

        public async Task<List<PlaylistMonitoringDto>> GetMonitoredPlaylists()
        {
            return await _context.PlaylistMonitorings.ToListAsync();
        }

        public async Task<List<StationMonitoringDto>> GetMonitoredStations()
        {
            return await _context.StationMonitorings.ToListAsync();
        }

        public async Task SavePlaylistSnapshots(List<PlaylistSnapshotDto> playlistSnapshots)
        {
            _context.PlaylistSnapshots.AddRange(playlistSnapshots);

            await _context.SaveChangesAsync();
        }

        public async Task SavePlaylistSnapshotSongs(List<PlaylistSnapshotSongDto> playlistSnapshots)
        {
            _context.PlaylistSnapshotSongs.AddRange(playlistSnapshots);

            await _context.SaveChangesAsync();
        }

        public async Task SaveStationSnapshotPlaylists(List<StationSnapshotPlaylistDto> stationSnapshotPlaylists)
        {
            _context.StationSnapshotPlaylists.AddRange(stationSnapshotPlaylists);

            await _context.SaveChangesAsync();
        }

        public async Task SaveStationSnapshots(List<StationSnapshotDto> stationSnapshots)
        {
            _context.StationSnapshots.AddRange(stationSnapshots);

            await _context.SaveChangesAsync();
        }

        public async Task SavePlaylistsMonitrorings(List<PlaylistMonitoringDto> playlistMonitorings)
        {
            await _context.BulkInsertOrUpdateAsync(playlistMonitorings);

            await _context.SaveChangesAsync();
        }
    }
}
