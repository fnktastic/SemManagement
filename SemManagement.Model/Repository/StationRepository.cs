﻿using Microsoft.EntityFrameworkCore;
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

        Task<List<SongExtended>> GetDeletedSongsAsync(int stationId);

        Task<List<SongExtended>> GetStationSongsAsync(int stationId);

        Task<List<StationQueue>> GetStationQueueAsync(int stationId);

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

        public async Task<List<SongExtended>> GetDeletedSongsAsync(int stationId)
        {
            var stationIdParameter = new MySqlParameter("@stationId", SqlDbType.Int)
            {
                Value = stationId
            };

            return await _context.SongsDeleteds.FromSql<SongExtended>(
                "SELECT songs.*, playlists.* " +
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
                "SELECT songs.*, playlists.* FROM stationsplaylists " +
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
