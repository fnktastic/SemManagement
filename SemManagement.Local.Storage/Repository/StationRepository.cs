using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SemManagement.LocalContext.DataAccess;
using SemManagement.LocalContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.LocalContext.Repository
{
    public interface IStationRepository
    {
        Task SaveRangeAsync(List<StationDto> stations);

        Task SaveAsync(StationDto station);
    }

    public class StationRepository : IStationRepository
    {
        private LocalDbContext _context;

        public StationRepository(LocalDbContext context)
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
