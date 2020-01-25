using Quartz;
using SemManagement.MonitoringContext.Repository;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SemManagement.API.Scheduler.Jobs
{
    public class HelloJob : Quartz.IJob
    {
        private readonly IMonitoringRepositry _monitoringRepositry;

        public HelloJob(IMonitoringRepositry monitoringRepositry)
        {
            _monitoringRepositry = monitoringRepositry;
        }


        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => Debug.WriteLine("Boom"));
        }
    }
}
