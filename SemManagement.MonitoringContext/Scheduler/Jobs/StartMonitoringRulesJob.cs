using Quartz;
using SemManagement.MonitoringContext.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Scheduler.Jobs
{
    public class StartMonitoringRulesJob : IJob
    {
        private readonly IRuleService _ruleService;

        public StartMonitoringRulesJob(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _ruleService.FireRules();
        }
    }
}
