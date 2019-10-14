using SemManagement.UWP.Model;
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
        Task<List<SongsDeleted>> GetDeletedSongsAsync(int stationId);

    }
    public class StationService : IStationService
    {
        private readonly IStationProvider _stationProvider;

        public StationService(IStationProvider stationProvider)
        {
            _stationProvider = stationProvider;
        }

        public async Task<List<SongsDeleted>> GetDeletedSongsAsync(int stationId)
        {
            return await _stationProvider.GetDeletedSongsAsync(stationId);
        }

        public async Task<List<Station>> TakeAsync(int take, int skip = 0)
        {
            return await _stationProvider.TakeAsync(take, skip);
        }
    }
}
