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
        List<Song> Take(int take, int skip = 0);
    }

    public class SongRepository : ISongRepository
    {
        private readonly Context _context;

        public SongRepository(Context context)
        {
            _context = context;
        }

        public List<Song> Take(int take, int skip = 0)
        {
            if (skip > 0)
                return _context.Songs.Skip(skip).Take(take).ToList();

            return _context.Songs.Take(take).ToList();
        }
    }
}
