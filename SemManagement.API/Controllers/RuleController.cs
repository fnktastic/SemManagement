using Microsoft.AspNetCore.Mvc;
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
            var rules =  await _ruleService.GetAllRulesAsync();

            return rules;
        }    
    }
}
