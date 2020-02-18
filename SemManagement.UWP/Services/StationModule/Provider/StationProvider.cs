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
        Task<List<SongExtended>> GetDeletedSongsAsync(int stationId);
        Task<List<SongExtended>> GetStationSongsAsync(int stationId);
        Task<User> GetStationUserAsync(int stationId);
        Task<Model.Local.Storage.Count> CountAsync();
        Task<List<StationQueue>> GetStationQueueAsync(int stationId);
        Task<Stationsstatus> GetStationStatuses(int sid);
        Task<List<ScheduledStation>> GetStationSchedule(int stationId);
        Task<List<Station>> GetStationsByPlaylist(int plid);
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

        public Task<List<Station>> GetStationsByPlaylist(int plid)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getStationsByPlaylist");

            return GetStationsByPlaylist<Station>(endpoint, plid);
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

        public Task<Model.Local.Storage.Count> CountAsync()
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "count");

            return CountAsync<Model.Local.Storage.Count>(endpoint);
        }

        public Task<List<StationQueue>> GetStationQueueAsync(int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getStationQueueAsync");

            return GetStationQueueAsync<StationQueue>(endpoint: endpoint, stationId: stationId);
        }

        public Task<Stationsstatus> GetStationStatuses(int sid)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getStationStatuses");

            return GetStationStatuses(endpoint, sid);
        }

        public Task<List<ScheduledStation>> GetStationSchedule(int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Stations, "getStationSchedule");

            return GetStationSchedule<ScheduledStation>(endpoint, stationId);
        }
    }
}
