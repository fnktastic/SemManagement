using SemManagement.UWP.Model;
using SemManagement.UWP.Services.PlaylistModule.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.PlaylistModule.Service
{
    public interface IPlaylistService
    {
        Task<List<Playlist>> TakeAsync(int take, int skip = 0);
        Task<Model.Local.Storage.Count> CountAsync();
        Task<List<Playlist>> GetPlaylistsByStationAsync(int stationId);
        Task RemovePlaylistFromStationAsync(int playlistId, int stationId);
        Task AddPlaylistToStationAsync(int playlistId, int stationId);
        Task SendSongToPlaylistsAsync(int sgid, List<Playlist> playlists);
        Task RemovePlaylistAsync(int plid);
    }

    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistProvider _playlistProvider;

        public PlaylistService(IPlaylistProvider playlistProvider)
        {
            _playlistProvider = playlistProvider;
        }

        public Task<Model.Local.Storage.Count> CountAsync()
        {
            return _playlistProvider.CountAsync();
        }

        public Task<List<Playlist>> TakeAsync(int take, int skip = 0)
        {
            return _playlistProvider.TakeAsync(take, skip);
        }

        public Task<List<Playlist>> GetPlaylistsByStationAsync(int stationId)
        {
            return _playlistProvider.GetPlaylistsByStationAsync(stationId);
        }

        public Task RemovePlaylistFromStationAsync(int playlistId, int stationId)
        {
            return _playlistProvider.RemovePlaylistFromStationAsync(playlistId, stationId);
        }

        public Task AddPlaylistToStationAsync(int playlistId, int stationId)
        {
            return _playlistProvider.AddPlaylistToStationAsync(playlistId, stationId);
        }

        public Task SendSongToPlaylistsAsync(int sgid, List<Playlist> playlists)
        {
            return _playlistProvider.SendSongToPlaylistsAsync(sgid, playlists);
        }

        public Task RemovePlaylistAsync(int plid)
        {
            return _playlistProvider.RemovePlaylistAsync(plid);
        }
    }
}
