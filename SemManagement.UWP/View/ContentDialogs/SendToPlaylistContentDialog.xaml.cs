using SemManagement.UWP.Helper;
using SemManagement.UWP.Model;
using SemManagement.UWP.ViewModel.ContentDialog;
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

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace SemManagement.UWP.View.ContentDialogs
{
    public sealed partial class SendToPlaylistContentDialog : ContentDialog
    {
        public SendToPlaylistViewModel SendToPlaylistViewModel { get; private set; }

        public SendToPlaylistContentDialog(SendToPlaylistViewModel vm)
        {
            SendToPlaylistViewModel = vm;
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void Stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaticSettings.StopSelectionChangedEvent == true) return;

            foreach (var item in e.AddedItems)
            {
                if (item is Playlist playlist)
                    SendToPlaylistViewModel.SelectedPlaylists.Add(playlist);
            }

            foreach (var item in e.RemovedItems)
            {
                if (item is Playlist playlist)
                    SendToPlaylistViewModel.SelectedPlaylists.Remove(playlist);
            }
        }
    }
}
