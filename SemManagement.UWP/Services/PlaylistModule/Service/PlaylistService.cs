using SemManagement.UWP.Model;
using SemManagement.UWP.Model.Api;
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
        Task<Count> CountAsync();
        Task<List<Playlist>> GetPlaylistsByStationAsync(int stationId);
        Task RemovePlaylistFromStationAsync(int playlistId, int stationId);

        Task AddPlaylistToStationAsync(int playlistId, int stationId);
    }

    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistProvider _playlistProvider;

        public PlaylistService(IPlaylistProvider playlistProvider)
        {
            _playlistProvider = playlistProvider;
        }

        public Task<Count> CountAsync()
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
    }
}
