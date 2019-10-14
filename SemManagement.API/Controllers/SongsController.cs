using Microsoft.AspNetCore.Mvc;
using SemManagement.Model.Model;
using SemManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongRepository _songRepository;

        public SongsController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        [HttpGet("get")]
        public async Task<ActionResult<IList<Song>>> TakeAsync(int skip, int take)
        {
            if (skip == 0 && take > 0)
                return await _songRepository.TakeAsync(take);

            if (skip > 0 && take > 0)
                return await _songRepository.TakeAsync(take, skip);

            return NotFound();
        }
    }
}
