using Microsoft.AspNetCore.Mvc;
using SemManagement.SemContext.Model;
using SemManagement.SemContext.Repository;
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

        [HttpGet("take")]
        public async Task<ActionResult<IList<Song>>> TakeAsync(int take, int skip)
        {
            if (skip == 0 && take > 0)
                return await _songRepository.TakeAsync(take);

            if (skip > 0 && take > 0)
                return await _songRepository.TakeAsync(take, skip);

            return NotFound();
        }

        [HttpGet("mostPopularSongs")]
        public async Task<ActionResult<IList<Song>>> MostPopularSongsAsync(int stationId, int top = 10)
        {
            return await _songRepository.MostPopularSongsAsync(stationId, top);
        }

        [HttpGet("getSongsByPlaylist")]
        public async Task<ActionResult<IList<Song>>> GetSongsByPlaylistAsync(int playlistId)
        {
            return await _songRepository.GetSongsByPlaylistAsync(playlistId);
        }
    }
}
