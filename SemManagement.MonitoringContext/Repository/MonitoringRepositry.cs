using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SemManagement.MonitoringContext.BusinessLogic;
using SemManagement.MonitoringContext.DataAccess;
using SemManagement.MonitoringContext.Enum;
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

        Task SaveStationPlayerStateRangeAsync(List<StationPlayerStateDto> stationsPlayerState);
        Task<FeedList> GetQucikMonitoringForStaton(List<int> plids, int sid);
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

        public async Task SaveStationPlayerStateRangeAsync(List<StationPlayerStateDto> stationsPlayerState)
        {
            await _context.BulkInsertAsync(stationsPlayerState);

            await _context.SaveChangesAsync();
        }

        public async Task<FeedList> GetQucikMonitoringForStaton(List<int> plids, int sid)
        {
            var feedList = new FeedList();

            var modifiedPlayistsSnapshots = await _context
                .PlaylistSnapshots
                .Where(x => plids.Contains(x.PlaylistId))
                .Include(x => x.SnapshotSongs)
                .ToListAsync();

            foreach (var stationPlayistsSnapshot in modifiedPlayistsSnapshots)
            {
                var playlistFeedItem = stationPlayistsSnapshot.ToFeedItem();

                if (stationPlayistsSnapshot.SnapshotAction != SnapshotActionEnum.None)
                    feedList.Add(playlistFeedItem);

                var songsFeedItems = stationPlayistsSnapshot.SnapshotSongs.ToFeedItems(stationPlayistsSnapshot.PlaylistName);

                feedList.AddRange(songsFeedItems);
            }


            var stationSnapshots = await _context.StationSnapshots
                .Where(x => x.StationId == sid)
                .Include(x => x.SnapshotPlaylists)
                .ToListAsync();

            foreach (var stationSnapshot in stationSnapshots)
            {
                var stationPlaylistFeedItems = stationSnapshot.SnapshotPlaylists.ToFeedItems();

                feedList.AddRange(stationPlaylistFeedItems);
            }

            return feedList;
        }
    }
}
