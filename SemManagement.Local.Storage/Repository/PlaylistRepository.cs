using EFCore.BulkExtensions;
using SemManagement.Local.Storage.DataAccess;
using SemManagement.Local.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Repository
{
    public interface IPlaylistRepository
    {
        Task SaveRangeAsync(List<PlaylistDto> playlists);
    }

    public class PlaylistRepository : IPlaylistRepository
    {
        private LocalStorageContext _context;

        public PlaylistRepository(LocalStorageContext context)
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
