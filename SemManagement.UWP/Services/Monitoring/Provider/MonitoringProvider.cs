﻿using System;
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
        Task AddMonitoringAsync(Model.Local.Storage.Monitoring monitoring);
        Task<List<Model.Local.Storage.Monitoring>> GetMonitoredStations();
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
    }
}
