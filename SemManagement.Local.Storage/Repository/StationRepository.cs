using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
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

        Task SaveAsync(StationDto station);
    }

    public class StationRepository : IStationRepository
    {
        private LocalStorageContext _context;

        public StationRepository(LocalStorageContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(StationDto station)
        {
            var existedStation = await _context.Stations.FirstOrDefaultAsync(x => x.Sid == station.Sid);

            if (existedStation == null)
            {
                _context.Stations.Add(station);

                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveRangeAsync(List<StationDto> stations)
        {
            await _context.BulkInsertOrUpdateAsync(stations);

            await _context.SaveChangesAsync();
        }
    }
}
