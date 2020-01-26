using Microsoft.AspNetCore.Mvc;
using Quartz;
using SemManagement.MonitoringContext.Model;
using SemManagement.MonitoringContext.Repository;
using SemManagement.MonitoringContext.Services;
using System.Threading.Tasks;

namespace SemManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly ISchedulerService _schedulerService;

        public MonitoringController(ISchedulerService schedulerService) 
        {
            _schedulerService = schedulerService;
        }

        [HttpPost("addMonitoringStation")]
        public async Task<ActionResult> MonitorStation([FromBody] StationMonitoring model)
        {
            if(ModelState.IsValid)
            {
                await _schedulerService.ScheduleMonitoring(model);

                return Ok();
            }

            return BadRequest();
        }
    }
}
