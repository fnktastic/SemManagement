using SemManagement.MonitoringContext.Repository;
using SemManagement.SemContext.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Services
{
    public interface IMonitoringService
    {
        Task MonitorPlaylists();
    }

    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringRepositry _monitoringRepositry;
        private readonly IPlaylistRepository _playlistRepository;

        public MonitoringService(IMonitoringRepositry monitoringRepositry, IPlaylistRepository playlistRepository)
        {
            _monitoringRepositry = monitoringRepositry;
            _playlistRepository = playlistRepository;
        }

        public async Task MonitorPlaylists()
        {
            var playlists = await  _playlistRepository.GetPlaylistsByStationAsync(138);
        }
    }
}
