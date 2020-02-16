using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SemManagement.MonitoringContext.DataAccess;
using SemManagement.MonitoringContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Repository
{
    public interface ILocalPlaylistRepository
    {
        Task SaveRangeAsync(List<PlaylistDto> playlists);
        Task SaveAsync(PlaylistDto playlist);
    }

    public class LocalPlaylistRepository : ILocalPlaylistRepository
    {
        private MonitoringDbContext _context;

        public LocalPlaylistRepository(MonitoringDbContext context)
        {
            _context = context;
        }
        public async Task SaveAsync(PlaylistDto playlist)
        {
            var existedPlaylist = await _context.Playlists.FirstOrDefaultAsync(x => x.Plid == playlist.Plid);

            if (existedPlaylist == null)
            {
                _context.Playlists.Add(playlist);

                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveRangeAsync(List<PlaylistDto> playlists)
        {
            await _context.BulkInsertOrUpdateAsync(playlists);

            await _context.SaveChangesAsync();
        }
    }
}
