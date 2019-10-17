using SemManagement.UWP.Configurations;
using SemManagement.UWP.Model;
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
        Task<List<SongsDeleted>> GetDeletedSongsAsync(int stationId);
        Task<List<Song>> GetStationSongsAsync(int stationId);
        Task<User> GetStationUserAsync(int stationId);
    }

    public class StationProvider : WebApiProvider, IStationProvider
    {
        public StationProvider(IRestEndpoints restEndpoints, PublicApiConfiguration settings) : base(restEndpoints, settings)
        {
        }

        public Task<List<Station>> TakeAsync(int take, int skip = 0)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "get");

            return TakeAsync<Station>(endpoint, take, skip);
        }

        public Task<List<SongsDeleted>> GetDeletedSongsAsync(int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getDeletedSongs");

            return GetDeletedSongsAsync<SongsDeleted>(endpoint, stationId);
        }

        public Task<List<Song>> GetStationSongsAsync(int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getStationSongsAsync");

            return GetStationSongsAsync<Song>(endpoint, stationId);
        }

        public Task<User> GetStationUserAsync(int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getStationUserAsync");

            return GetStationUserAsync(endpoint, stationId);
        }
    }
}
