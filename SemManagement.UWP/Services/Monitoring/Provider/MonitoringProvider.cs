using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemManagement.UWP.Configurations;
using SemManagement.UWP.Model;
using SemManagement.UWP.Model.Local.Storage;
using SemManagement.UWP.Services.Monitoring.Provider;

namespace SemManagement.UWP.Services.Monitoring.Provider
{
    public interface IMonitoringProvider
    {
        Task AddMonitoringAsync(Model.Local.Storage.Monitoring monitoring);
        Task<List<Model.Local.Storage.Monitoring>> GetMonitoredStations();
        Task<BoolResult> RunMonitoringNow();
        Task<BoolResult> StartMonitoring();
        Task<FeedList> GetQucikMonitoringForStaton(int sid);
        Task<FeedList> GetQucikMonitoring(DateTime dateTime);
    }

    public class MonitoringProvider : WebApiProvider, IMonitoringProvider
    {
        public MonitoringProvider(IRestEndpoints restEndpoints, PublicApiConfiguration settings) : base(restEndpoints, settings)
        {

        }

        public Task AddMonitoringAsync(Model.Local.Storage.Monitoring monitoring)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Monitoring, "addMonitoringStation");

            return AddAsync<Model.Local.Storage.Monitoring>(endpoint, monitoring);
        }

        public Task<List<Model.Local.Storage.Monitoring>> GetMonitoredStations()
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Monitoring, "getMonitorings");

            return GetMonitoredStations<Model.Local.Storage.Monitoring>(endpoint);
        }

        public Task<BoolResult> RunMonitoringNow()
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Monitoring, "coldStart");

            return RunMonitoringNow<BoolResult>(endpoint);
        }

        public Task<BoolResult> StartMonitoring()
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Monitoring, "start");

            return StartMonitoring<BoolResult>(endpoint);
        }

        public Task<FeedList> GetQucikMonitoringForStaton(int sid)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Monitoring, "getQucikMonitoringForStaton");

            return GetQucikMonitoringForStaton<FeedList>(endpoint, sid);
        }

        public Task<FeedList> GetQucikMonitoring(DateTime dateTime)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Monitoring, "getQucikMonitoring");

            return GetQucikMonitoring<FeedList>(endpoint, dateTime);
        }
    }
}
