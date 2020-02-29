using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SemManagement.SemContext.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.SemContext.Repository
{
    public interface IPlaylistRepository
    {
        Task<List<Playlist>> TakeAsync(int take = 0, int skip = 0, DateTime? lastSnapshotAt = null);

        Task<Count> CountAsync();

        Task<List<Playlist>> GetPlaylistsByStationAsync(int stationId, DateTime? lastSnapshotAt = null);

        Task<int> RemovePlaylistFromStationAsync(int playlistId, int stationId);

        Task<int> AddPlaylistToStationAsync(int playlistId, int stationId);

        Task<bool> CheckIfPlaylistAssignedToStation(int playlistId, int stationId);

        Task<List<Playlistssongs>> GetModifiedPlaylistsSongs(DateTime? lastSnapshotAt = null);

        Task<Playlist> GetPlaylistById(int plid);

        Task<List<StationsPlaylists>> GetModifiedPlaylistsByStationAsync(int stationId, DateTime lastSnapshotAt);

        Task<BoolResult> SendSongToPlaylistsAsync(int sgid, List<Playlist> playlists);

        Task<BoolResult> RemovePlaylistAsync(int plid);

        Task<List<Playlist>> GetModifiedPlaylists(DateTime? lastSnapshotAt = null);

        Task<List<StationsPlaylists>> GetModifiedStationsPlaylistsAsync(DateTime lastSnapshotAt);
    }

    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly SemDbContext _context;

        public PlaylistRepository(SemDbContext context)
        {
            _context = context;
        }

        public async Task<Count> CountAsync()
        {
            int count = await _context.Playlists.CountAsync();

            return new Count(count);
        }

        public Task<List<Playlist>> TakeAsync(int take = 0, int skip = 0, DateTime? lastSnapshotAt = null)
        {
            var playlistsQuery = _context.Playlists.AsQueryable();

            if (lastSnapshotAt.HasValue)
            {
                playlistsQuery = playlistsQuery.Where(x => x.Last_Update_Date > lastSnapshotAt.Value);
            }

            if (take == 0 && skip == 0)
                return playlistsQuery.ToListAsync();

            if (skip > 0)
                return playlistsQuery.Skip(skip).Take(take).ToListAsync();

            return playlistsQuery.Take(take).ToListAsync();
        }

        public Task<List<Playlistssongs>> GetModifiedPlaylistsSongs(DateTime? lastSnapshotAt = null)
        {
            var modifiedPlaylistsSongs = _context.Playlistssongs
                .Where(x => x.Last_Update_Date > lastSnapshotAt.Value)
                .ToListAsync();

            return modifiedPlaylistsSongs;
        }

        public Task<List<Playlist>> GetModifiedPlaylists(DateTime? lastSnapshotAt = null)
        {
            var modifiedPlaylists = _context.Playlists
                .Where(x => x.Last_Update_Date > lastSnapshotAt.Value)
                .ToListAsync();

            return modifiedPlaylists;
        }

        public async Task<Playlist> GetPlaylistById(int plid)
        {
            var playlists = await _context.Playlists
                .Where(x => x.Plid == plid)
                .ToListAsync();

            if (playlists.Count > 0)
                return playlists.First();

            return new Playlist();
        }

        public Task<List<Playlist>> GetPlaylistsByStationAsync(int stationId, DateTime? lastSnapshotAt = null)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            var playlistsQuery = _context.Playlists.FromSql<Playlist>(
                "SELECT playlists.* " +
                "FROM stationsplaylists " +
                "INNER JOIN playlists ON playlists.plid = stationsplaylists.plid " +
                "WHERE sid = @stationId", stationIdParameter);

            if (lastSnapshotAt.HasValue)
            {
                playlistsQuery = playlistsQuery.Where(x => x.Last_Update_Date > lastSnapshotAt.Value);
            }

            var playlists = playlistsQuery.ToListAsync();

            return playlists;
        }

        public async Task<List<StationsPlaylists>> GetModifiedPlaylistsByStationAsync(int stationId, DateTime lastSnapshotAt)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            var lastSnapshotAtParameter = new MySqlParameter("@lastSnapshotAt", MySqlDbType.DateTime)
            {
                Value = lastSnapshotAt.ToString("yyyy-MM-dd HH:mm:ss")
            };

            var stationPlaylistsQuery = _context.StationsPlaylists.FromSql<StationsPlaylists>(
                "SELECT stationsplaylists.*, playlists.name FROM sem.playlists " +
                "INNER JOIN stationsplaylists ON playlists.plid = stationsplaylists.plid " +
                "WHERE stationsplaylists.sid = @stationId AND stationsplaylists.last_update_date >= @lastSnapshotAt", stationIdParameter, lastSnapshotAtParameter);

            var stationPlaylists = await stationPlaylistsQuery.ToListAsync();

            return stationPlaylists;
        }

        public async Task<List<StationsPlaylists>> GetModifiedStationsPlaylistsAsync(DateTime lastSnapshotAt)
        {
            var lastSnapshotAtParameter = new MySqlParameter("@lastSnapshotAt", MySqlDbType.DateTime)
            {
                Value = lastSnapshotAt.ToString("yyyy-MM-dd HH:mm:ss")
            };

            var stationPlaylistsQuery = _context.StationsPlaylists.FromSql<StationsPlaylists>(
                "SELECT stationsplaylists.*, playlists.name FROM sem.playlists " +
                "INNER JOIN stationsplaylists ON playlists.plid = stationsplaylists.plid " +
                "WHERE stationsplaylists.last_update_date >= @lastSnapshotAt", lastSnapshotAtParameter);

            var stationPlaylists = await stationPlaylistsQuery.ToListAsync();

            return stationPlaylists;
        }

        public async Task<int> RemovePlaylistFromStationAsync(int playlistId, int stationId)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            var playlistIdParameter = new MySqlParameter("@playlistId", SqlDbType.Int)
            {
                Value = playlistId
            };

            return await _context.Database.ExecuteSqlCommandAsync(
                "DELETE FROM sem.stationsplaylists " +
                "WHERE sid =  @stationId AND plid = @playlistId ", stationIdParameter, playlistIdParameter);
        }

        public async Task<bool> CheckIfPlaylistAssignedToStation(int playlistId, int stationId)
        {
            var stationPlaylists = await _context.StationsPlaylists
                .Where(x => x.Plid == playlistId && x.Sid == stationId)
                .ToListAsync();

            if (stationPlaylists.Count > 0)
                return true;

            return false;
        }

        public async Task<int> AddPlaylistToStationAsync(int playlistId, int stationId)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            var playlistIdParameter = new MySqlParameter("@playlistId", SqlDbType.Int)
            {
                Value = playlistId
            };

            return await _context.Database.ExecuteSqlCommandAsync(
                "INSERT INTO sem.stationsplaylists(sid, plid, syncronized) " +
                "VALUES(@stationId, @playlistId, 0) ", stationIdParameter, playlistIdParameter);
        }

        public async Task<BoolResult> SendSongToPlaylistsAsync(int sgid, List<Playlist> playlists)
        {
            foreach(var playlist in playlists)
            {
                var songs = (await _context.Playlistssongs.Where(x => x.Plid == playlist.Plid).ToListAsync())
                    .OrderBy(x => x.Position)
                    .ToList();

                int max = songs.Max(x => x.Position);

                max++;

                _context.Playlistssongs.Add(new Playlistssongs()
                {
                    Plid = playlist.Plid,
                    Sgid = sgid, 
                    Position = max,
                    Last_Update_Date = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();

            return new BoolResult();
        }

        public async Task<BoolResult> RemovePlaylistAsync(int plid)
        {
            var item = _context.Playlists.Find(plid);

            if(item != null)
            {
                _context.Entry<Playlist>(item).State = EntityState.Deleted;

                await _context.SaveChangesAsync();

                return new BoolResult(true);
            }

            return new BoolResult(false);
        }
    }
}
