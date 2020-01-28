using EFCore.BulkExtensions;
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
    }

    public class LocalPlaylistRepository : ILocalPlaylistRepository
    {
        private MonitoringDbContext _context;

        public LocalPlaylistRepository(MonitoringDbContext context)
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
