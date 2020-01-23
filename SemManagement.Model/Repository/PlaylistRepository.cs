using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SemManagement.Model.DataAccess;
using SemManagement.Model.Model;
using SemManagement.Model.Model.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Model.Repository
{
    public interface IPlaylistRepository
    {
        Task<List<Playlist>> TakeAsync(int take = 0, int skip = 0);
        Task<Count> CountAsync();

        Task<List<Playlist>> GetPlaylistsByStationAsync(int stationId);

        Task<int> RemovePlaylistFromStationAsync(int playlistId, int stationId);
    }

    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly SemContext _context;

        public PlaylistRepository(SemContext context)
        {
            _context = context;
        }

        public async Task<Count> CountAsync()
        {
            int count = await _context.Playlists.CountAsync();

            return new Count(count);
        }

        public Task<List<Playlist>> TakeAsync(int take = 0, int skip = 0)
        {
            if (take == 0 && skip == 0)
                return _context.Playlists.ToListAsync();

            if (skip > 0)
                return _context.Playlists.Skip(skip).Take(take).ToListAsync();

            return _context.Playlists.Take(take).ToListAsync();
        }

        public Task<List<Playlist>> GetPlaylistsByStationAsync(int stationId)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            var playlists = _context.Playlists.FromSql<Playlist>(
                "SELECT playlists.* " +
                "FROM stationsplaylists " +
                "INNER JOIN playlists ON playlists.plid = stationsplaylists.plid " +
                "WHERE sid = @stationId", stationIdParameter)
                .ToListAsync();

            return playlists;
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
    }
}
