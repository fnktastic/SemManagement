using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SemManagement.Model.Model;
using SemManagement.Model.Repository;

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

        [HttpGet("get")]
        public async Task<ActionResult<IList<Station>>> TakeAsync(int take, int skip)
        {
            if (skip == 0 && take > 0)
                return await _stationRepository.TakeAsync(take);

            if (skip > 0 && take > 0)
                return await _stationRepository.TakeAsync(take, skip);

            return NotFound();
        }

        [HttpGet("getDeletedSongs")]
        public async Task<ActionResult<IList<SongsDeleted>>> GetDeletedSongsAsync(int stationId)
        {
            if (stationId != 0)
                return await _stationRepository.GetDeletedSongsAsync(stationId);

            return NotFound();
        }

        [HttpGet("getStationSongsAsync")]
        public async Task<ActionResult<IList<Song>>> GetStationSongsAsync(int stationId)
        {
            if (stationId != 0)
                return await _stationRepository.GetStationSongsAsync(stationId);

            return NotFound();
        }

        [HttpGet("getStationUserAsync")]
        public async Task<ActionResult<User>> GetStationUserAsync(int stationId)
        {
            if (stationId != 0)
                return await _stationRepository.GetStationUserAsync(stationId);

            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
