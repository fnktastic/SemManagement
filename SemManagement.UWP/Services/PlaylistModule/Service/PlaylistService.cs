﻿using SemManagement.UWP.Model;
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
    }

    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistProvider _playlistProvider;

        public PlaylistService(IPlaylistProvider playlistProvider)
        {
            _playlistProvider = playlistProvider;
        }

        public Task<List<Playlist>> TakeAsync(int take, int skip = 0)
        {
            return _playlistProvider.TakeAsync(take, skip);
        }
    }
}
