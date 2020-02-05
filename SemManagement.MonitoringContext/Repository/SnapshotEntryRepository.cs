using SemManagement.MonitoringContext.DataAccess;
using SemManagement.MonitoringContext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Repository
{
    public interface ISnapshotEntryRepository
    {
        Task InsertAsync(SnapshotEntryDto snapshotEntry);
    }

    public class SnapshotEntryRepository : ISnapshotEntryRepository
    {
        private MonitoringDbContext _context;

        public SnapshotEntryRepository(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(SnapshotEntryDto snapshotEntry)
        {
            _context.SnapshotEntries.Add(snapshotEntry);

            await _context.SaveChangesAsync();
        }
    }
}
