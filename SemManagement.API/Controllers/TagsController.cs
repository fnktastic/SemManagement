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
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("saveStationTagRangeAsync")]
        public async Task<ActionResult> SaveStationTagRangeAsync([FromBody] TagViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _tagService.SaveStationTagRangeAsync(model.Station, model.Tags);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("getAllTagsAsync")]
        public async Task<ActionResult<List<TagDto>>> GetAllTagsAsync([FromQuery] int sid)
        {
            return await _tagService.GetAllTagsAsync(sid);
        }

        [HttpGet("getStationByTagsAsync")]
        public async Task<ActionResult<List<StationDto>>> GetStationByTagsAsync([FromBody] List<TagDto> tags)
        {
            if (ModelState.IsValid)
            {
                return await _tagService.GetStationByTagsAsync(tags);
            }

            return BadRequest();
        }
    }
}
