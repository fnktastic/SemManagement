using AutoMapper;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using SemManagement.Local.Storage.DataAccess;
using SemManagement.Local.Storage.Repository;
using SemManagement.UWP.Configurations;
using SemManagement.UWP.Helper;
using SemManagement.UWP.Services.Local.RuleModule;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.PlaylistModule.Provider;
using SemManagement.UWP.Services.PlaylistModule.Service;
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
            SimpleIoc.Default.Register<StationsViewModel>();
            SimpleIoc.Default.Register<PlaylistsViewModel>();
            SimpleIoc.Default.Register<AudiosViewModel>();
            SimpleIoc.Default.Register<RulesViewModel>();

            SimpleIoc.Default.Register<PublicApiConfiguration>();
            SimpleIoc.Default.Register<IRestEndpoints, RestEndpoints>();
            SimpleIoc.Default.Register<ISongProvider, SongProvider>();
            SimpleIoc.Default.Register<ISongService, SongService>();
            SimpleIoc.Default.Register<IStationProvider, StationProvider>();
            SimpleIoc.Default.Register<IStationService, StationService>();
            SimpleIoc.Default.Register<IPlaylistProvider, PlaylistProvider>();
            SimpleIoc.Default.Register<IPlaylistService, PlaylistService>();

            SimpleIoc.Default.Register<IConfigurationProvider, MyConfig>();
            SimpleIoc.Default.Register<IMapper, MyMapper>();

            //local storage
            SimpleIoc.Default.Register<LocalStorageContext>();
            SimpleIoc.Default.Register<IRulesRepository, RulesRepository>();
            SimpleIoc.Default.Register<ILocalDataService, LocalDataService>();
            SimpleIoc.Default.Register<IRuleService, RuleService>();

            SetupNavigation();
        }

        private static void SetupNavigation()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("StartPageStations", typeof(StationsPage));
            navigationService.Configure("StartPageAudios", typeof(AudiosPage));
            navigationService.Configure("StartPageSettings", typeof(SettingsPage));
            navigationService.Configure("StartPagePlaylists", typeof(PlaylistsPage));
            navigationService.Configure("StartPageRules", typeof(RulesPage));

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

        public StationsViewModel StationsPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StationsViewModel>();
            }
        }

        public PlaylistsViewModel PlaylistsPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlaylistsViewModel>();
            }
        }

        public AudiosViewModel AudiosPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AudiosViewModel>();
            }
        }

        public RulesViewModel RulesPageInstance

        {
            get
            {
                return ServiceLocator.Current.GetInstance<RulesViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
