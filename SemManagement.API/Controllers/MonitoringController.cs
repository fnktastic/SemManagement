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
        private readonly IMonitoringService _monitoringService;

        public MonitoringController(IMonitoringService monitoringService) 
        {
            _monitoringService = monitoringService;
        }

        [HttpPost("addMonitoringStation")]
        public async Task<ActionResult> MonitorStation([FromBody] StationMonitoring model)
        {
            if(ModelState.IsValid)
            {
                await _monitoringService.AddMonitoringStation(model);

                return Ok();
            }

            return BadRequest();
        }
    }
}
