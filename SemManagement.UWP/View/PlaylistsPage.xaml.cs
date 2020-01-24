﻿using SemManagement.UWP.Helper;
using SemManagement.UWP.Model;
using SemManagement.UWP.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SemManagement.UWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlaylistsPage : Page
    {
        public PlaylistsPage()
        {
            this.InitializeComponent();
        }

        private void Playlists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaticSettings.StopSelectionChangedEvent == true) return;

            if(DataContext is PlaylistsViewModel playlistsViewModel)
            {
                foreach (var item in e.AddedItems)
                {
                    if (item is Playlist playlist)
                        playlistsViewModel.SelectedPlaylists.Add(playlist);
                }

                foreach (var item in e.RemovedItems)
                {
                    if (item is Playlist playlist)
                        playlistsViewModel.SelectedPlaylists.Remove(playlist);
                }
            }
        }

        private void Audios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaticSettings.StopSelectionChangedEvent == true) return;

            if (DataContext is PlaylistsViewModel playlistsViewModel)
            {
                foreach (var item in e.AddedItems)
                {
                    if (item is Song song)
                        playlistsViewModel.SelectedSongs.Add(song);
                }

                foreach (var item in e.RemovedItems)
                {
                    if (item is Song song)
                        playlistsViewModel.SelectedSongs.Remove(song);
                }
            }
        }
    }
}
