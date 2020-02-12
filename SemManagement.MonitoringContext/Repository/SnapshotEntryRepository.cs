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
        Task InsertAsync(MonitoringDto monitoringDto);

        Task<MonitoringDto> GetLast(MonitorTypeEnum  monitorType, MonitorStateEnum monitorState);
    }

    public class SnapshotEntryRepository : ISnapshotEntryRepository
    {
        private MonitoringDbContext _context;

        public SnapshotEntryRepository(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task<MonitoringDto> GetLast(MonitorTypeEnum monitorType, MonitorStateEnum monitorState)
        {
            var snapshotEntries = await _context.Monitorings
                .Where(x => x.MonitorType == monitorType && x.MonitorState == monitorState)
                .OrderByDescending(x => x.Timestamp)
                .Take(1)
                .ToListAsync();

            if(snapshotEntries.Any())
                return snapshotEntries.FirstOrDefault();

            var firstEntry = new MonitoringDto()
            {
                MonitorState = monitorState,
                MonitorType = monitorType,
                Timestamp = DateTime.Now
            };

            _context.Monitorings.Add(firstEntry);

            await _context.SaveChangesAsync();

            return firstEntry;
        }

        public async Task InsertAsync(MonitoringDto snapshotEntry)
        {
            _context.Monitorings.Add(snapshotEntry);

            await _context.SaveChangesAsync();
        }
    }
}
