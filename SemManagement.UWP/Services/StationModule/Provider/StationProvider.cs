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

        public async Task<List<Station>> TakeAsync(int take, int skip = 0)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "get");

            return await TakeAsync<Station>(endpoint, take, skip);
        }

        public async Task<List<SongsDeleted>> GetDeletedSongsAsync(int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getDeletedSongs");

            return await GetDeletedSongsAsync<SongsDeleted>(endpoint, stationId);
        }

        public Task<List<Song>> GetStationSongsAsync(int stationId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetStationUserAsync(int stationId)
        {
            throw new NotImplementedException();
        }
    }
}
