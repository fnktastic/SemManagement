using EFCore.BulkExtensions;
using SemManagement.LocalContext.DataAccess;
using SemManagement.LocalContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.LocalContext.Repository
{
    public interface IPlaylistRepository
    {
        Task SaveRangeAsync(List<PlaylistDto> playlists);
    }

    public class PlaylistRepository : IPlaylistRepository
    {
        private LocalDbContext _context;

        public PlaylistRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task SaveRangeAsync(List<PlaylistDto> playlists)
        {
            await _context.BulkInsertOrUpdateAsync(playlists);

            await _context.SaveChangesAsync();
        }
    }
}
