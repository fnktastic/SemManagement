using SemManagement.Model.DataAccess;
using SemManagement.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemManagement.Model.Repository
{
    public interface IStationRepository
    {
        List<Station> GetTop(int top);
    }
    public class StationRepository : IStationRepository
    {
        private readonly Context _context;

        public StationRepository(Context context)
        {
            _context = context;
        }

        public List<Station> GetTop(int top)
        {
            return _context.Stations.Take(top).ToList();
        }
    }
}
