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
    public interface ISongRepository
    {
        Task<List<Song>> TakeAsync(int take, int skip = 0);

        Task<List<SongStat>> MostPopularSongsAsync(int stationId, int top = 10);

        Task<List<Song>> GetSongsByPlaylistAsync(int playlistId, DateTime? lastSnapshotAt = null);

        Task<List<Song>> GetModifiedSongsByPlaylistAsync(int playlistId, DateTime? lastSnapshotAt = null);
    }

    public class SongRepository : ISongRepository
    {
        private readonly SemDbContext _context;

        public SongRepository(SemDbContext context)
        {
            _context = context;
        }

        public Task<List<Song>> GetSongsByPlaylistAsync(int playlistId, DateTime? lastSnapshotAt = null)
        {
            var playlistIdParameter = new MySqlParameter("@playlistId", SqlDbType.Int)
            {
                Value = playlistId
            };

            var audiosQuery = _context.Songs.FromSql<Song>(
                "SELECT songs.* FROM songs " +
                "INNER JOIN playlistssongs ON songs.sgid = playlistssongs.sgid " +
                "WHERE playlistssongs.plid = @playlistId", playlistIdParameter);

            if (lastSnapshotAt.HasValue)
            {
                audiosQuery = audiosQuery.Where(x => x.Last_Update_Date > lastSnapshotAt.Value);
            }

            var audios = audiosQuery.ToListAsync();

            return audios;
        }

        public Task<List<Song>> GetModifiedSongsByPlaylistAsync(int playlistId, DateTime? lastSnapshotAt = null)
        {
            var playlistIdParameter = new MySqlParameter("@playlistId", SqlDbType.Int)
            {
                Value = playlistId
            };

            var lastSnapshotAtParameter = new MySqlParameter("@lastSnapshotAt", MySqlDbType.DateTime)
            {
                Value = lastSnapshotAt.Value.ToString("yyyy-MM-dd HH:mm:ss")
            };

            var audiosQuery = _context.Songs.FromSql<Song>(
                "SELECT songs.* FROM songs " +
                "INNER JOIN playlistssongs ON songs.sgid = playlistssongs.sgid " +
                "WHERE playlistssongs.plid = @playlistId AND playlistssongs.last_update_date >= @lastSnapshotAt", playlistIdParameter, lastSnapshotAtParameter);

            var audios = audiosQuery.ToListAsync();

            return audios;
        }

        public async Task<List<SongStat>> MostPopularSongsAsync(int stationId, int top = 10)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            var topParameter = new MySqlParameter("@top", SqlDbType.Int)
            {
                Value = top
            };

            return await _context.SongStats.FromSql<SongStat>(
                "SELECT s1.*, COUNT(s1.sgid) count FROM songs AS s1 " +
                "INNER JOIN playlistssongs as pl1 ON s1.sgid = pl1.sgid " +
                "INNER JOIN stationsplaylists as sp1 ON sp1.plid = pl1.plid " +
                "WHERE sp1.sid = @stationId " +
                "GROUP BY s1.sgid " +
                "ORDER BY count DESC " +
                "LIMIT @top", stationIdParameter, topParameter)
                .ToListAsync();
        }

        public Task<List<Song>> TakeAsync(int take, int skip = 0)
        {
            if (skip > 0)
                return _context.Songs.Skip(skip).Take(take).ToListAsync();

            return _context.Songs.Take(take).ToListAsync();
        }
    }
}
