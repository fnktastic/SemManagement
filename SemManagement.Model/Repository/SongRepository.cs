using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SemManagement.Model.DataAccess;
using SemManagement.Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Model.Repository
{
    public interface ISongRepository
    {
        Task<List<Song>> TakeAsync(int take, int skip = 0);

        Task<List<Song>> MostPopularSongsAsync(int stationId, int top = 10);
    }

    public class SongRepository : ISongRepository
    {
        private readonly SemContext _context;

        public SongRepository(SemContext context)
        {
            _context = context;
        }

        public async Task<List<Song>> MostPopularSongsAsync(int stationId, int top = 10)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            var topParameter = new MySqlParameter("@top", SqlDbType.Int)
            {
                Value = top
            };

            return await _context.Songs.FromSql<Song>(
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
