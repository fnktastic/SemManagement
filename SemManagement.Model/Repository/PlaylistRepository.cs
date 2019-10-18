using Microsoft.EntityFrameworkCore;
using SemManagement.Model.DataAccess;
using SemManagement.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Model.Repository
{
    public interface IPlaylistRepository
    {
        Task<List<Playlist>> TakeAsync(int take = 0, int skip = 0);
    }

    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly Context _context;

        public PlaylistRepository(Context context)
        {
            _context = context;
        }

        public Task<List<Playlist>> TakeAsync(int take = 0, int skip = 0)
        {
            if (take == 0 && skip == 0)
                return _context.Playlists.ToListAsync();

            if (skip > 0)
                return _context.Playlists.Skip(skip).Take(take).ToListAsync();

            return _context.Playlists.Take(take).ToListAsync();
        }
    }
}
