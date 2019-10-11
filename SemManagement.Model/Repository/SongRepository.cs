using SemManagement.Model.DataAccess;
using SemManagement.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemManagement.Model.Repository
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
            return _context.Songs.Take(top).ToList();
            /* var mySqlParameter = new MySqlParameter("top", MySqlDbType.Int32)
             {
                 Value = top
             };

             return _context.Songs.SqlQuery("SELECT * FROM Songs LIMIT @top", mySqlParameter).ToList();*/
        }
    }
}
