using SemManagement.UWP.Model;
using SemManagement.UWP.Services.SongModule.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.SongModule.Service
{
    public interface ISongService
    {
        Task<List<Song>> TakeAsync(int take, int skip = 0);
    }

    public class SongService : ISongService
    {
        private readonly ISongProvider _songProvider;

        public SongService(ISongProvider songProvider)
        {
            _songProvider = songProvider;
        }

        public Task<List<Song>> TakeAsync(int take, int skip = 0)
        {
            return _songProvider.TakeAsync(take, skip);
        }
    }
}
