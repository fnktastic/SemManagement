using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using SemManagement.UWP.Configurations;
using SemManagement.UWP.Helper;
using SemManagement.UWP.Services.SongModule.Provider;
using SemManagement.UWP.Services.SongModule.Service;
using SemManagement.UWP.Services.StationModule.Provider;
using SemManagement.UWP.Services.StationModule.Service;
using SemManagement.UWP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SemManagement.UWP.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
            }
            else
            {
                // Create run time view services and models
            }

            SimpleIoc.Default.Register<StartPageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();

            SimpleIoc.Default.Register<PublicApiConfiguration>();
            SimpleIoc.Default.Register<IRestEndpoints, RestEndpoints>();
            SimpleIoc.Default.Register<ISongProvider, SongProvider>();
            SimpleIoc.Default.Register<ISongService, SongService>();
            SimpleIoc.Default.Register<IStationProvider, StationProvider>();
            SimpleIoc.Default.Register<IStationService, StationService>();

            SetupNavigation();
        }

        private static void SetupNavigation()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("StartPageStations", typeof(StationsPage));
            navigationService.Configure("StartPageAudios", typeof(AudiosPage));
            navigationService.Configure("StartPageSettings", typeof(SettingsPage));

            SimpleIoc.Default.Register<NavigationService>(() => navigationService);
        }

        public StartPageViewModel StartPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartPageViewModel>();
            }
        }

        public SettingsPageViewModel SettingsPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsPageViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
