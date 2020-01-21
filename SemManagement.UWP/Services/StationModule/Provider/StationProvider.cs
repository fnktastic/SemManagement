using SemManagement.UWP.Configurations;
using SemManagement.UWP.Model;
using SemManagement.UWP.Model.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.StationModule.Provider
{
    public interface IStationProvider
    {
        Task<List<Station>> TakeAsync(int take, int skip = 0);
        Task<List<SongExtended>> GetDeletedSongsAsync(int stationId);
        Task<List<SongExtended>> GetStationSongsAsync(int stationId);
        Task<User> GetStationUserAsync(int stationId);
        Task<Count> CountAsync();
        Task<List<StationQueue>> GetStationQueueAsync(int stationId);
    }

    public class StationProvider : WebApiProvider, IStationProvider
    {
        public StationProvider(IRestEndpoints restEndpoints, PublicApiConfiguration settings) : base(restEndpoints, settings)
        {
        }

        public Task<List<Station>> TakeAsync(int take, int skip = 0)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "take");

            return TakeAsync<Station>(endpoint, take, skip);
        }

        public Task<List<SongExtended>> GetDeletedSongsAsync(int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getDeletedSongs");

            return GetDeletedSongsAsync<SongExtended>(endpoint, stationId);
        }

        public Task<List<SongExtended>> GetStationSongsAsync(int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getStationSongsAsync");

            return GetStationSongsAsync<SongExtended>(endpoint, stationId);
        }

        public Task<User> GetStationUserAsync(int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getStationUserAsync");

            return GetStationUserAsync(endpoint, stationId);
        }

        public Task<Count> CountAsync()
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "count");

            return CountAsync<Count>(endpoint);
        }

        public Task<List<StationQueue>> GetStationQueueAsync(int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getStationQueueAsync");

            return GetStationQueueAsync<StationQueue>(endpoint: endpoint, stationId: stationId);
        }
    }
}
