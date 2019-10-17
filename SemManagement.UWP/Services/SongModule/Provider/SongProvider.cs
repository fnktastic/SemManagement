using SemManagement.UWP.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemManagement.UWP.Model;
using System.IO;

namespace SemManagement.UWP.Services.SongModule.Provider
{
    public interface ISongProvider
    {
        Task<List<Song>> TakeAsync(int take, int skip = 0);

        Task<List<Song>> MostPopularSongs(int stationId, int top = 10);
    }

    public class SongProvider : WebApiProvider, ISongProvider
    {
        public SongProvider(IRestEndpoints restEndpoints, PublicApiConfiguration settings) : base(restEndpoints, settings)
        {
        }

        public Task<List<Song>> TakeAsync(int take, int skip = 0)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Songs, "get");

            return TakeAsync<Song>(endpoint, take, skip);
        }

        public Task<List<Song>> MostPopularSongs(int stationId, int top = 10)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Songs, "mostPopularSongs");

            return MostPopularSongs<Song>(endpoint, stationId, top);
        }
    }
}
