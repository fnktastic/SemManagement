using Microsoft.AspNetCore.Mvc;
using SemManagement.Model.Model;
using SemManagement.Model.Model.Api;
using SemManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistRepository _playlistRepository;
        public PlaylistsController(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        [HttpGet("take")]
        public async Task<ActionResult<IList<Playlist>>> TakeAsync(int take, int skip)
        {
            if (skip == 0 && take > 0)
                return await _playlistRepository.TakeAsync(take);

            if (skip > 0 && take > 0)
                return await _playlistRepository.TakeAsync(take, skip);

            return await _playlistRepository.TakeAsync();
        }

        [HttpGet("count")]
        public async Task<ActionResult<Count>> CountAsync()
        {
            return await _playlistRepository.CountAsync();
        }
    }
}
