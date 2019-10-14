using Microsoft.EntityFrameworkCore;
using SemManagement.Model.DataAccess;
using SemManagement.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Model.Repository
{
    public interface ISongRepository
    {
        Task<List<Song>> TakeAsync(int take, int skip = 0);
    }

    public class SongRepository : ISongRepository
    {
        private readonly Context _context;

        public SongRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Song>> TakeAsync(int take, int skip = 0)
        {
            if (skip > 0)
                return await _context.Songs.Skip(skip).Take(take).ToListAsync();

            return await _context.Songs.Take(take).ToListAsync();
        }        
    }
}
