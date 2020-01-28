using Microsoft.AspNetCore.Mvc;
using SemManagement.MonitoringContext.Model;
using SemManagement.MonitoringContext.Services;
using SemManagement.MonitoringContext.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IRuleService _ruleService;

        public RuleController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        [HttpPost("saveRule")]
        public async Task<ActionResult> SaveRuleAsync([FromBody] RuleViewModel ruleViewModel)
        {
            await _ruleService.SaveRuleAsync(ruleViewModel);

            return Ok();
        }

        [HttpGet("getAllRules")]
        public async Task<ActionResult<List<RuleViewModel>>> GetAllRulesAsync()
        {
            return await _ruleService.GetAllRulesAsync();
        }

        [HttpPost("getRuleLogs")]
        public async Task<ActionResult<List<RuleLogDto>>> GetRuleLogs([FromQuery] Guid ruleId)
        {
            return await _ruleService.GetRuleLogs(ruleId);
        }

        [HttpPost("fireRule")]
        public async Task<ActionResult<List<RuleLogDto>>> FireRule([FromQuery] Guid ruleId)
        {
            await _ruleService.FireRule(ruleId);

            return Ok();
        }
    }
}
