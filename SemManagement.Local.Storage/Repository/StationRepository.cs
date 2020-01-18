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
    public interface IStationRepository
    {
        Task SaveRangeAsync(List<StationDto> stations);
    }

    public class StationRepository : IStationRepository
    {
        private LocalStorageContext _context;

        public StationRepository(LocalStorageContext context)
        {
            _context = context;
        }

        public async Task SaveRangeAsync(List<StationDto> stations)
        {
            await _context.BulkInsertOrUpdateAsync(stations);

            await _context.SaveChangesAsync();
        }
    }
}
