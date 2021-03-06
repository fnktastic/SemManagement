﻿using SemManagement.UWP.Model;
using SemManagement.UWP.Services.StationModule.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.StationModule.Service
{
    public interface IStationService
    {
        Task<List<Station>> TakeAsync(int take, int skip = 0);
        Task<List<SongExtended>> GetDeletedSongsAsync(int stationId);
        Task<List<SongExtended>> GetStationSongsAsync(int stationId);
        Task<User> GetStationUserAsync(int stationId);
        Task<Model.Local.Storage.Count> CountAsync();
        Task<List<StationQueue>> GetStationQueueAsync(int stationId);
        Task<Stationsstatus> GetStationStatuses(int sid);
        Task<List<ScheduledStation>> GetStationSchedule(int stationId);
        Task<List<Station>> GetStationsByPlaylist(int plid);

    }
    public class StationService : IStationService
    {
        private readonly IStationProvider _stationProvider;

        public StationService(IStationProvider stationProvider)
        {
            _stationProvider = stationProvider;
        }

        public Task<List<SongExtended>> GetDeletedSongsAsync(int stationId)
        {
            return _stationProvider.GetDeletedSongsAsync(stationId);
        }

        public Task<List<Station>> TakeAsync(int take, int skip = 0)
        {
            return _stationProvider.TakeAsync(take, skip);
        }

        public Task<List<SongExtended>> GetStationSongsAsync(int stationId)
        {
            return _stationProvider.GetStationSongsAsync(stationId);
        }

        public Task<User> GetStationUserAsync(int stationId)
        {
            return _stationProvider.GetStationUserAsync(stationId);
        }

        public Task<Model.Local.Storage.Count> CountAsync()
        {
            return _stationProvider.CountAsync();
        }

        public Task<List<StationQueue>> GetStationQueueAsync(int stationId)
        {
            return _stationProvider.GetStationQueueAsync(stationId);
        }

        public Task<Stationsstatus> GetStationStatuses(int sid)
        {
            return _stationProvider.GetStationStatuses(sid);
        }

        public Task<List<ScheduledStation>> GetStationSchedule(int stationId)
        {
            return _stationProvider.GetStationSchedule(stationId);
        }

        public Task<List<Station>> GetStationsByPlaylist(int plid)
        {
            return _stationProvider.GetStationsByPlaylist(plid);
        }
    }
}
