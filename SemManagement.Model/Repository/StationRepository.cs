using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SemManagement.Model.DataAccess;
using SemManagement.Model.Model;
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
    }
    public class StationRepository : IStationRepository
    {
        private readonly Context _context;

        public StationRepository(Context context)
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
                "WHERE sid = @stationId", stationIdParameter)
                .ToListAsync();
        }
    }
}
