using Microsoft.AspNetCore.Mvc;
using Quartz;
using SemManagement.API.Scheduler;
using SemManagement.API.Scheduler.Jobs;
using SemManagement.MonitoringContext.Repository;
using System.Threading.Tasks;

namespace SemManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly IMonitoringRepositry _monitoringRepositry;
        private readonly MonitoringScheduler _monitoringScheduler;

        public MonitoringController(IMonitoringRepositry monitoringRepositry, MonitoringScheduler monitoringScheduler) 
        {
            _monitoringRepositry = monitoringRepositry;
            _monitoringScheduler = monitoringScheduler;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            _monitoringScheduler.AddJob<HelloJob>("test", "testGroup", 5);

            //JobManager.AddJob(new HelloJob(_monitoringRepositry), i => i.ToRunEvery(5).Seconds());

            //JobManager.AddJob(() => Console.WriteLine("Late job!"), (s) => s.ToRunEvery(5).Seconds());

            return await Task.FromResult<OkResult>(Ok());
        }
    }
}
