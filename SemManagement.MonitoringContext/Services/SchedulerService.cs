﻿using Quartz;
using SemManagement.MonitoringContext.Model;
using SemManagement.MonitoringContext.Repository;
using SemManagement.MonitoringContext.Scheduler;
using SemManagement.MonitoringContext.Scheduler.Jobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Services
{
    public interface ISchedulerService
    {
        Task ScheduleMonitoring(StationMonitoringDto stationMonitoring);
        Task StartMonitorStations();
        Task StartRule(RuleDto rule);
    }
    public class SchedulerService : ISchedulerService
    {
        private readonly IMonitoringRepositry _monitoringRepositry;
        private readonly IMonitoringScheduler _monitoringScheduler;

        public SchedulerService(IMonitoringScheduler monitoringScheduler, IMonitoringRepositry monitoringRepositry)
        {
            _monitoringScheduler = monitoringScheduler;
            _monitoringRepositry = monitoringRepositry;
        }

        public async Task ScheduleMonitoring(StationMonitoringDto stationMonitoring)
        {
            bool exists = await _monitoringRepositry.CheckIfExist(stationMonitoring);

            if (exists) return;

            await _monitoringRepositry.AddMonitoringStation(stationMonitoring);

            _monitoringScheduler.AddJob<StartMonitoringJob>(
                  string.Format("stations_{0}", stationMonitoring.StationId),
                  "stations",
                  stationMonitoring.RepeatInterval,
                  stationMonitoring.StartDateTime.Value,
                  stationMonitoring.StationId);
        }

        public async Task StartMonitorStations()
        {
            var stations = await _monitoringRepositry.GetMonitoredStations();

            foreach(var station in stations)
            {
                _monitoringScheduler.AddJob<StartMonitoringJob>(
                    string.Format("stations_{0}", station.StationId),
                    "stations",
                    station.RepeatInterval,
                    station.StartDateTime.Value,
                    station.StationId);
            }
        }

        public async Task StartRule(RuleDto rule)
        {
            await Task.Run(() => _monitoringScheduler.AddContiniousJob<SetUpRuleJob>(
                   string.Format("rules_{0}", rule.Id),
                   "rules",
                   rule.Id.ToString()
                ));
        }
    }
}
