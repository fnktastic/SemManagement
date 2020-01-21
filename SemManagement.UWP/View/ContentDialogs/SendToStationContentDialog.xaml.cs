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
    public sealed partial class SendToStationContentDialog : ContentDialog
    {
        public SendToStationViewModel SendToStationViewModel { get; private set; }

        public SendToStationContentDialog(SendToStationViewModel vm)
        {
            SendToStationViewModel = vm;
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
                if (item is Station station)
                    SendToStationViewModel.SelectedStations.Add(station);
            }

            foreach (var item in e.RemovedItems)
            {
                if (item is Station station)
                    SendToStationViewModel.SelectedStations.Remove(station);
            }
        }
    }
}
