using SemManagement.UWP.Helper;
using SemManagement.UWP.Model;
using SemManagement.UWP.ViewModel.ContentDialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SemManagement.UWP.View.ContentDialogs
{
    public sealed partial class AddRuleContentDialog : ContentDialog
    {
        public AddRuleViewModel AddRuleViewModel { get; private set; }

        public AddRuleContentDialog(AddRuleViewModel vm)
        {
            AddRuleViewModel = vm;
            this.InitializeComponent();

            sourcePlaylists.ItemsSource = AddRuleViewModel.SourcePlaylists;
        }

        private void SourcePlaylists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaticSettings.StopSelectionChangedEvent == true) return;

            foreach (var item in e.AddedItems)
            {
                if (item is Playlist playlist)
                    AddRuleViewModel.SelectedSourcePlaylists.Add(playlist);
            }

            foreach (var item in e.RemovedItems)
            {
                if (item is Playlist playlist)
                    AddRuleViewModel.SelectedSourcePlaylists.Remove(playlist);
            }
        }

        private void TargetPlaylists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaticSettings.StopSelectionChangedEvent == true) return;

            foreach (var item in e.AddedItems)
            {
                if (item is Playlist playlist)
                    AddRuleViewModel.SelectedTargetPlaylists.Add(playlist);
            }

            foreach (var item in e.RemovedItems)
            {
                if (item is Playlist playlist)
                    AddRuleViewModel.SelectedTargetPlaylists.Remove(playlist);
            }
        }

        private void Stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaticSettings.StopSelectionChangedEvent == true) return;

            foreach (var item in e.AddedItems)
            {
                if (item is Station station)
                    AddRuleViewModel.SelectedStations.Add(station);
            }

            foreach (var item in e.RemovedItems)
            {
                if (item is Station station)
                    AddRuleViewModel.SelectedStations.Remove(station);
            }
        }
    }
}
