using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using SemManagement.UWP.Helper;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.SongModule.Service;
using SemManagement.UWP.Services.StationModule.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SemManagement.UWP.ViewModel
{
    public class StartPageViewModel : ViewModelBase
    {
        #region fields
        private readonly NavigationService _navigationService;

        private readonly ISongService _songService;

        private readonly IStationService _stationService;
        #endregion

        #region properties
    
        #endregion

        #region commands
        private RelayCommand<NavigationViewItemInvokedEventArgs> _itemInvokedCommand;
        public RelayCommand<NavigationViewItemInvokedEventArgs> ItemInvokedCommand => this._itemInvokedCommand ?? (this._itemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked));
        private void OnItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                _navigationService.NavigateTo("StartPageSettings");
                return;
            }

            _navigationService.NavigateTo(args.InvokedItemContainer.Tag.ToString());
        }

        private RelayCommand _loadedCommand;
        public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(Loaded));
        private void Loaded()
        {
            _navigationService.CurrentFrame = UIHelper.GetMainFrame();
        }
        #endregion

        #region constructor
        public StartPageViewModel(NavigationService navigationService, ISongService songService, IStationService stationService)
        {
            _navigationService = navigationService;
            _songService = songService;
            _stationService = stationService;
        }
        #endregion

        #region private methods

        #endregion
    }
}
