using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SemManagement.SemContext.Api;
using SemManagement.SemContext.Model;
using SemManagement.SemContext.Repository;

namespace SemManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly IStationRepository _stationRepository;

        public StationsController(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        [HttpGet("take")]
        public async Task<ActionResult<IList<Station>>> TakeAsync(int take, int skip)
        {
            if (skip == 0 && take > 0)
                return await _stationRepository.TakeAsync(take);

            if (skip > 0 && take > 0)
                return await _stationRepository.TakeAsync(take, skip);

            return NotFound();
        }

        [HttpGet("getDeletedSongs")]
        public async Task<ActionResult<IList<SongExtended>>> GetDeletedSongsAsync(int stationId)
        {
            if (stationId != 0)
                return await _stationRepository.GetDeletedSongsAsync(stationId);

            return NotFound();
        }

        [HttpGet("getStationSongsAsync")]
        public async Task<ActionResult<IList<SongExtended>>> GetStationSongsAsync(int stationId)
        {
            if (stationId != 0)
                return await _stationRepository.GetStationSongsAsync(stationId);

            return NotFound();
        }

        [HttpGet("getStationQueueAsync")]
        public async Task<ActionResult<IList<StationQueue>>> GetStationQueueAsync(int stationId)
        {
            if (stationId != 0)
                return await _stationRepository.GetStationQueueAsync(stationId);

            return NotFound();
        }

        [HttpGet("getStationUserAsync")]
        public async Task<ActionResult<User>> GetStationUserAsync(int stationId)
        {
            if (stationId != 0)
                return await _stationRepository.GetStationUserAsync(stationId);

            return NotFound();
        }

        [HttpGet("count")]
        public async Task<ActionResult<Count>> CountAsync()
        {
            return await _stationRepository.CountAsync();
        }

        [HttpGet("getStationStatuses")]
        public async Task<ActionResult<Stationsstatus>> GetStationStatuses([FromQuery] int sid)
        {
            return await _stationRepository.GetStationStatuses(sid);
        }

        [HttpGet("getStationSchedule")]
        public async Task<List<ScheduledStation>> GetStationSchedule([FromQuery] int stationId)
        {
            return await _stationRepository.GetStationSchedule(stationId);
        }
        
    }
}
