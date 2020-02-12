using Microsoft.EntityFrameworkCore;
using SemManagement.MonitoringContext.DataAccess;
using SemManagement.MonitoringContext.Enum;
using SemManagement.MonitoringContext.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Repository
{
    public interface ISnapshotEntryRepository
    {
        Task InsertAsync(SnapshotEntryDto snapshotEntry);

        Task<SnapshotEntryDto> GetLast(SnapshotTypeEnum snapshotType, SnapshotEntryStateEnum snapshotEntryState);
    }

    public class SnapshotEntryRepository : ISnapshotEntryRepository
    {
        private MonitoringDbContext _context;

        public SnapshotEntryRepository(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task<SnapshotEntryDto> GetLast(SnapshotTypeEnum snapshotType, SnapshotEntryStateEnum snapshotEntryState)
        {
            var snapshotEntries = await _context.SnapshotEntries
                .Where(x => x.SnapshotType == snapshotType && x.EntryState == snapshotEntryState)
                .OrderByDescending(x => x.DateTime)
                .Take(1)
                .ToListAsync();

            if(snapshotEntries.Any())
                return snapshotEntries.FirstOrDefault();

            var firstEntry = new SnapshotEntryDto()
            {
                EntryState = snapshotEntryState,
                SnapshotType = snapshotType,
                DateTime = DateTime.Now
            };

            _context.SnapshotEntries.Add(firstEntry);

            await _context.SaveChangesAsync();

            return firstEntry;
        }

        public async Task InsertAsync(SnapshotEntryDto snapshotEntry)
        {
            _context.SnapshotEntries.Add(snapshotEntry);

            await _context.SaveChangesAsync();
        }
    }
}
