using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SemManagement.SemContext.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SemManagement.SemContext.Repository
{
    public interface IStationRepository
    {
        Task<List<Station>> TakeAsync(int take, int skip = 0);

        Task<List<SongExtended>> GetDeletedSongsAsync(int stationId);

        Task<List<SongExtended>> GetStationSongsAsync(int stationId);

        Task<List<StationQueue>> GetStationQueueAsync(int stationId);

        Task<User> GetStationUserAsync(int stationId);

        Task<Count> CountAsync();

        Task<List<Station>> GetAllActiveStations();

        Task<Stationsstatus> GetStationStatuses(int sid);

        Task<List<ScheduledStation>> GetStationSchedule(int stationId);
    }
    public class StationRepository : IStationRepository
    {
        private readonly SemDbContext _context;

        public StationRepository(SemDbContext context)
        {
            _context = context;
        }

        public async Task<List<Station>> TakeAsync(int take, int skip = 0)
        {
            if (skip > 0)
                return await _context.Stations.Skip(skip).Take(take).ToListAsync();

            return await _context.Stations.Take(take).ToListAsync();
        }

        public async Task<List<SongExtended>> GetDeletedSongsAsync(int stationId)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            return await _context.SongsDeleteds.FromSql<SongExtended>(
                "SELECT songs.*, playlists.plid, playlists.name, playlists.changed " +
                "FROM songsdeleted " +
                "INNER JOIN songs ON songs.sgid = songsdeleted.sgid " +
                "INNER JOIN playlists ON playlists.plid = songsdeleted.plid " +
                "WHERE songsdeleted.sid = @stationId", stationIdParameter)
                .ToListAsync();
        }

        public async Task<List<SongExtended>> GetStationSongsAsync(int stationId)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            return await _context.SongsDeleteds.FromSql<SongExtended>(
                "SELECT songs.*,  playlists.plid, playlists.name, playlists.changed FROM stationsplaylists " +
                "INNER JOIN playlists ON playlists.plid = stationsplaylists.plid " +
                "INNER JOIN playlistssongs ON playlistssongs.plid = playlists.plid " +
                "INNER JOIN songs ON songs.sgid = playlistssongs.sgid " +
                "WHERE stationsplaylists.sid = @stationId", stationIdParameter)
                .ToListAsync();
        }

        public async Task<List<StationQueue>> GetStationQueueAsync(int stationId)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            return await _context.StationQueues.FromSql<StationQueue>(
                "SELECT songs.*, stationsqueue.position, stationsqueue.creation_date, stationsqueue.semid FROM sem.stationsqueue  " +
                "INNER JOIN songs ON songs.sgid = stationsqueue.sgid " +
                "WHERE sid = @stationId", stationIdParameter)
                .ToListAsync();
        }

        public async Task<List<Station>> GetAllActiveStations()
        {
            return await _context.Stations.Where(x => x.Blocked == 0).ToListAsync();
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

        public async Task<Stationsstatus> GetStationStatuses(int sid)
        {
            var stationStatuses = await _context.Stationsstatuses.Where(x => x.idStation == sid).ToListAsync();

            if(stationStatuses != null && stationStatuses.Count > 0)
            {
                return stationStatuses.First();
            }

            return new Stationsstatus();
        }

        public async Task<List<ScheduledStation>> GetStationSchedule(int stationId)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            return await _context.ScheduledStations.FromSql<ScheduledStation>(
                "select schedulerstations.scid, " +
                "schedulerstations.scpid, " +
                "schedulerstations.sid, " + 
                "schedulerstations.uid, " +
                "schedulerstations.name as scheduleName, " + 
                "schedulerstations.synced, " +
                "schedulerstations.exported, " +
                "schedulerstations.changed_date, " +
                "schedulerevents.start, " +
                "schedulerevents.stop, " +
                "schedulerevents.weekday, " +
                "schedulerevents.playmode, " +
                "schedulerevents.stopPlayback, " +
                "schedulerplaylists.plid, " +
                "schedulerplaylists.scevid, " +
                "schedulerplaylists.spid, " +
                "schedulerplaylists.last_update_date, " +
                "playlists.name " +
                "from schedulerstations " +
                "inner join schedulerevents on schedulerevents.scid = schedulerstations.scid " +
                "inner join schedulerplaylists on schedulerplaylists.scevid = schedulerevents.scevid " +
                "inner join playlists on playlists.plid = schedulerplaylists.plid " +
                "WHERE schedulerstations.sid = @stationId", stationIdParameter)
                .ToListAsync();
        }
    }
}
