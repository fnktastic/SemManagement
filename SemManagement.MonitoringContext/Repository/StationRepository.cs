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
    public interface ILocalStationRepository
    {
        Task SaveRangeAsync(List<StationDto> stations);

        Task SaveAsync(StationDto station);
    }

    public class LocalStationRepository : ILocalStationRepository
    {
        private MonitoringDbContext _context;

        public LocalStationRepository(MonitoringDbContext context)
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
            await _context.AddRangeAsync(stations); //  .BulkInsertOrUpdateAsync(stations);

            await _context.SaveChangesAsync();
        }
    }
}
