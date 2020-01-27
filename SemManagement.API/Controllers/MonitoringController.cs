using Microsoft.AspNetCore.Mvc;
using Quartz;
using SemManagement.MonitoringContext.Model;
using SemManagement.MonitoringContext.Repository;
using SemManagement.MonitoringContext.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SemManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly ISchedulerService _schedulerService;
        private readonly IMonitoringRepositry _monitoringRepositry;

        public MonitoringController(ISchedulerService schedulerService, IMonitoringRepositry monitoringRepositry) 
        {
            _schedulerService = schedulerService;
            _monitoringRepositry = monitoringRepositry;
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

        [HttpGet("start")]
        public async Task<ActionResult> Start()
        {
            if (ModelState.IsValid)
            {
                await _schedulerService.StartMonitorStations();

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("getMonitorings")]
        public async Task<ActionResult<List<StationMonitoring>>> GetMonitorings()
        {
            if (ModelState.IsValid)
            {
                return await _monitoringRepositry.GetMonitoredStations();
            }

            return BadRequest();
        }
    }
}
