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
        Task<Model.Local.Storage.Count> CountAsync();
        Task<List<Playlist>> GetPlaylistsByStationAsync(int stationId);
        Task RemovePlaylistFromStationAsync(int playlistId, int stationId);
        Task AddPlaylistToStationAsync(int playlistId, int stationId);
        Task SendSongToPlaylistsAsync(int sgid, List<Model.Playlist> playlists);
        Task RemovePlaylistAsync(int plid);
    }

    public class PlaylistProvider : WebApiProvider, IPlaylistProvider
    {
        public PlaylistProvider(IRestEndpoints restEndpoints, PublicApiConfiguration settings) : base(restEndpoints, settings)
        {

        }

        public Task<Model.Local.Storage.Count> CountAsync()
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Playlists, "count");

            return CountAsync<Model.Local.Storage.Count>(endpoint);
        }

        public Task<List<Playlist>> GetPlaylistsByStationAsync(int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Playlists, "getPlaylistsByStation");

            return GetPlaylistsByStationAsync<Playlist>(endpoint, stationId);
        }

        public Task RemovePlaylistFromStationAsync(int playlistId, int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Playlists, "removePlaylistFromStationAsync");

            return RemovePlaylistFromStationAsync(endpoint, playlistId, stationId);
        }

        public Task AddPlaylistToStationAsync(int playlistId, int stationId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Playlists, "addPlaylistToStationAsync");

            return RemovePlaylistFromStationAsync(endpoint, playlistId, stationId);
        }

        public Task<List<Playlist>> TakeAsync(int take, int skip = 0)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Playlists, "take");

            return TakeAsync<Playlist>(endpoint, take, skip);
        }

        public Task SendSongToPlaylistsAsync(int sgid, List<Model.Playlist> playlists)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Playlists, "sendSongToPlaylistsAsync");

            return SendSongToPlaylistsAsync<Model.Local.Storage.BoolResult>(endpoint, sgid, playlists);
        }

        public Task RemovePlaylistAsync(int plid)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Playlists, "removePlaylistAsync");

            return RemovePlaylistAsync(endpoint, plid);
        }
    }
}
