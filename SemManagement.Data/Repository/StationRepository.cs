using MySql.Data.MySqlClient;
using SemManagement.Data.DataAccess;
using SemManagement.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Data.Repository
{
    public interface IStationRepository
    {
        IList<Station> GetTop(int top);
    }
    public class StationRepository : IStationRepository
    {
        private readonly Context _context;

        public StationRepository(Context context)
        {
            _context = context;
        }

        public IList<Station> GetTop(int top)
        {
            var mySqlParameter = new MySqlParameter("top", MySqlDbType.Int32)
            {
                Value = top
            };

            return _context.Stations.SqlQuery("SELECT * FROM Stations LIMIT @top", mySqlParameter).ToList();
        }
    }
}
