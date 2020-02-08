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
            if (ModelState.IsValid)
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

        [HttpPost("getStationByTagsAsync")]
        public async Task<ActionResult<List<StationDto>>> GetStationByTagsAsync([FromBody] TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _tagService.GetStationByTagsAsync(model.Tags);
            }

            return BadRequest();
        }

        [HttpDelete("deleteStationTagByIdAsync")]
        public async Task<ActionResult<BoolResult>> DeleteStationTagByIdAsync([FromQuery] int stationId, Guid tagId)
        {
            if (ModelState.IsValid)
            {
                return await _tagService.DeleteStationTagByIdAsync(stationId, tagId);
            }

            return BadRequest();
        }
    }
}
