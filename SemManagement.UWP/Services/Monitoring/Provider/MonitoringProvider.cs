using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemManagement.UWP.Configurations;
using SemManagement.UWP.Model;

namespace SemManagement.UWP.Services.Monitoring.Provider
{
    public interface IMonitoringProvider
    {
        Task AddMonitoringAsync(Model.Monitoring monitoring);
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
    }
}
