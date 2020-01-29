using Quartz;
using SemManagement.MonitoringContext.Model;
using SemManagement.MonitoringContext.Repository;
using SemManagement.MonitoringContext.Scheduler;
using SemManagement.MonitoringContext.Scheduler.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Services
{
    public interface ISchedulerService
    {
        void StartMonitorStations();
        void StartMonitorRules();
        void StartMonitorPlaylists();
        void StartEchoJob();
    }
    public class SchedulerService : ISchedulerService
    {
        private readonly IMonitoringRepositry _monitoringRepositry;
        private readonly IMonitoringScheduler _monitoringScheduler;
        private readonly ILocalRulesRepository _localRulesRepository;

        public SchedulerService(ILocalRulesRepository localRulesRepository, IMonitoringScheduler monitoringScheduler, IMonitoringRepositry monitoringRepositry, IRuleService ruleService)
        {
            _monitoringScheduler = monitoringScheduler;
            _monitoringRepositry = monitoringRepositry;
            _localRulesRepository = localRulesRepository;
        }

        public void StartMonitorStations()
        {
                _monitoringScheduler.AddStationsJob<StartMonitoringJob>(
                    "stations",
                    "stations");
        }

        public void StartEchoJob()
        {
            _monitoringScheduler.AddEchoJob<EchoJob>("echo", "echo");
        }

        public void StartMonitorRules()
        {
            _monitoringScheduler.AddRulesJob<SetUpRuleJob>(
                   "rule",
                   "rule");
        }

        public void StartMonitorPlaylists()
        {
            _monitoringScheduler.AddPlaylistsJob<StartMonitoringPlaylistJob>(
                   "palylists",
                   "palylists");
        }
    }
}
