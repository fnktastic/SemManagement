using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SemManagement.Model.DataAccess;
using SemManagement.Model.Model;
using SemManagement.Model.Model.Api;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SemManagement.Model.Repository
{
    public interface IStationRepository
    {
        Task<List<Station>> TakeAsync(int take, int skip = 0);

        Task<List<SongsDeleted>> GetDeletedSongsAsync(int stationId);

        Task<List<Song>> GetStationSongsAsync(int stationId);

        Task<User> GetStationUserAsync(int stationId);

        Task<Count> CountAsync();
    }
    public class StationRepository : IStationRepository
    {
        private readonly SemContext _context;

        public StationRepository(SemContext context)
        {
            _context = context;
        }

        public async Task<List<Station>> TakeAsync(int take, int skip = 0)
        {
            if (skip > 0)
                return await _context.Stations.Skip(skip).Take(take).ToListAsync();

            return await _context.Stations.Take(take).ToListAsync();
        }

        public async Task<List<SongsDeleted>> GetDeletedSongsAsync(int stationId)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            return await _context.SongsDeleteds.FromSql<SongsDeleted>(
                "SELECT songs.*, playlists.* " +
                "FROM songsdeleted " +
                "INNER JOIN songs ON songs.sgid = songsdeleted.sgid " +
                "INNER JOIN playlists ON playlists.plid = songsdeleted.plid " +
                "WHERE songsdeleted.sid = @stationId", stationIdParameter)
                .ToListAsync();
        }

        public Task<List<Song>> GetStationSongsAsync(int stationId)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetStationUserAsync(int stationId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Count> CountAsync()
        {
            int count = await _context.Stations.CountAsync();

            return new Count(count);
        }
    }
}
