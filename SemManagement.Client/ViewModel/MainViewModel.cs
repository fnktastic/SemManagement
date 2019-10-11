using GalaSoft.MvvmLight;
using SemManagement.Data.DataAccess;
using SemManagement.Data.Repository;

namespace SemManagement.Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ISongRepository _songRepository;
        private readonly IStationRepository _stationRepository;
        private readonly IUserRepository _userRepository;

        public MainViewModel()
        {
            var context = new Context();
            _songRepository = new SongRepository(context);
            _stationRepository = new StationRepository(context);
            _userRepository = new UserRepository(context);

            var top1000songs = _songRepository.GetTop(1000);
            var top100users = _userRepository.GetTop(100);
            var top100stations = _stationRepository.GetTop(100);
        }
    }
}