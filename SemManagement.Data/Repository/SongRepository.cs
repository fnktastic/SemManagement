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
    public interface ISongRepository
    {
        IList<Song> GetTop(int top);
    }

    public class SongRepository : ISongRepository
    {
        private readonly Context _context;

        public SongRepository(Context context)
        {
            _context = context;
        }

        public IList<Song> GetTop(int top)
        {
            var mySqlParameter = new MySqlParameter("top", MySqlDbType.Int32)
            {
                Value = top
            };

            return _context.Songs.SqlQuery("SELECT * FROM Songs LIMIT @top", mySqlParameter).ToList();
        }
    }
}
