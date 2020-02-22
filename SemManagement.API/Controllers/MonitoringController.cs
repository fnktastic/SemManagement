using Microsoft.AspNetCore.Mvc;
using Quartz;
using SemManagement.MonitoringContext.BusinessLogic;
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
        private readonly IMonitoringService _monitoringService;

        public MonitoringController(IMonitoringService monitoringService, ISchedulerService schedulerService, IMonitoringRepositry monitoringRepositry) 
        {
            _monitoringService = monitoringService;
            _schedulerService = schedulerService;
            _monitoringRepositry = monitoringRepositry;
        }

        [HttpPost("addMonitoringStation")]
        public async Task<ActionResult> MonitorStation([FromBody] StationMonitoringDto model)
        {
            if(ModelState.IsValid)
            {
                await _monitoringRepositry.AddMonitoringStation(model);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("start")]
        public ActionResult Start()
        {
            if (ModelState.IsValid)
            {
                _schedulerService.StartEchoJob();

                _schedulerService.StartMonitorStations();

                _schedulerService.StartMonitorPlaylists();

                _schedulerService.StartMonitorRules();

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("coldStart")]
        public async Task<ActionResult<BoolResult>> ColdStart()
        {
            if (ModelState.IsValid)
            {
                var success = await _monitoringService.ColdStartMonitoring();

                return Ok(success);
            }

            return BadRequest();
        }

        [HttpGet("getMonitorings")]
        public async Task<ActionResult<List<StationMonitoringDto>>> GetMonitorings()
        {
            if (ModelState.IsValid)
            {
                return await _monitoringRepositry.GetMonitoredStations();
            }

            return BadRequest();
        }

        [HttpGet("getQucikMonitoringForStaton")]
        public async Task<ActionResult<FeedList>> GetQucikMonitoringForStaton([FromQuery] int sid)
        {
            if(ModelState.IsValid)
            {
                return await _monitoringService.GetQucikMonitoringForStaton(sid);
            }

            return BadRequest();
        }
    }
}
