using SemManagement.UWP.Configurations;
using SemManagement.UWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.PlaylistModule.Provider
{
    public interface IPlaylistProvider
    {
        Task<List<Playlist>> TakeAsync(int take, int skip = 0);
    }

    class PlaylistProvider : WebApiProvider, IPlaylistProvider
    {
        public PlaylistProvider(IRestEndpoints restEndpoints, PublicApiConfiguration settings) : base(restEndpoints, settings)
        {

        }

        public Task<List<Playlist>> TakeAsync(int take, int skip = 0)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Playlists, "take");

            return TakeAsync<Playlist>(endpoint, take, skip);
        }
    }
}
