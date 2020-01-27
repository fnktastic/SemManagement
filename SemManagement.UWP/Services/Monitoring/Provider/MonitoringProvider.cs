using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemManagement.UWP.Configurations;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Monitoring.Provider;

namespace SemManagement.UWP.Services.Monitoring.Provider
{
    public interface IMonitoringProvider
    {
        Task AddMonitoringAsync(Model.Monitoring monitoring);
        Task<List<Model.Monitoring>> GetMonitoredStations();
    }

    public class MonitoringProvider : WebApiProvider, IMonitoringProvider
    {
        public MonitoringProvider(IRestEndpoints restEndpoints, PublicApiConfiguration settings) : base(restEndpoints, settings)
        {

        }

        public Task AddMonitoringAsync(Model.Monitoring monitoring)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Monitoring, "addMonitoringStation");

            return AddAsync<Model.Monitoring>(endpoint, monitoring);
        }

        public Task<List<Model.Monitoring>> GetMonitoredStations()
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Monitoring, "getMonitorings");

            return GetMonitoredStations<Model.Monitoring>(endpoint);
        }
    }
}
