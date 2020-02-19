using AutoMapper;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using SemManagement.UWP.Configurations;
using SemManagement.UWP.Helper;
using SemManagement.UWP.Services.Local.Settings;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.Monitoring.Provider;
using SemManagement.UWP.Services.Monitoring.Service;
using SemManagement.UWP.Services.PlaylistModule.Provider;
using SemManagement.UWP.Services.PlaylistModule.Service;
using SemManagement.UWP.Services.RuleModule.Provider;
using SemManagement.UWP.Services.SongModule.Provider;
using SemManagement.UWP.Services.SongModule.Service;
using SemManagement.UWP.Services.StationModule.Provider;
using SemManagement.UWP.Services.StationModule.Service;
using SemManagement.UWP.Services.TagModule.Provider;
using SemManagement.UWP.Services.TagModule.Service;
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
            SimpleIoc.Default.Register<MonitoringViewModel>();
            SimpleIoc.Default.Register<FeedViewModel>();

            SimpleIoc.Default.Register<PublicApiConfiguration>();
            SimpleIoc.Default.Register<IRestEndpoints, RestEndpoints>();
            SimpleIoc.Default.Register<ISongProvider, SongProvider>();
            SimpleIoc.Default.Register<ISongService, SongService>();
            SimpleIoc.Default.Register<IStationProvider, StationProvider>();
            SimpleIoc.Default.Register<IStationService, StationService>();
            SimpleIoc.Default.Register<IPlaylistProvider, PlaylistProvider>();
            SimpleIoc.Default.Register<IPlaylistService, PlaylistService>();
            SimpleIoc.Default.Register<ISettingsService, SettingsService>();
            SimpleIoc.Default.Register<IMonitoringProvider, MonitoringProvider>();
            SimpleIoc.Default.Register<IMonitoringService, MonitoringService>();
            SimpleIoc.Default.Register<Services.RuleModule.Service.IRuleService, Services.RuleModule.Service.RuleService>();
            SimpleIoc.Default.Register<IRuleProvider, RuleProvider>();
            SimpleIoc.Default.Register<ITagProvider, TagProvider>();
            SimpleIoc.Default.Register<ITagService, TagService>();

            SimpleIoc.Default.Register<IConfigurationProvider, MyConfig>();
            SimpleIoc.Default.Register<IMapper, MyMapper>();

            //local storage
            SimpleIoc.Default.Register<ILocalDataService, LocalDataService>();

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
            navigationService.Configure("StartPageMonitoring", typeof(MonitoringPage));
            navigationService.Configure("StartFeed", typeof(FeedPage));

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

        public MonitoringViewModel MonitoringPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MonitoringViewModel>();
            }
        }

        public FeedViewModel FeedPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FeedViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
