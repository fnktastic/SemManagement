using AutoMapper;
using SemManagement.UWP.Model.Local.Storage;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.PlaylistModule.Service;
using SemManagement.UWP.Services.StationModule.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.Local.RuleModule
{
    public interface IRuleService
    {
        Task<Dictionary<Station, List<Playlist>>> ExtractPlaylists(Rule rule);
    }

    public class RuleService : IRuleService
    {
        private readonly ILocalDataService _localDataService;
        private readonly IPlaylistService _playlistService;
        private readonly IStationService _stationService;
        private readonly IMapper _mapper;

        public RuleService(ILocalDataService localDataService, IPlaylistService playlistService, IStationService stationService, IMapper mapper)
        {
            _localDataService = localDataService;
            _playlistService = playlistService;
            _stationService = stationService;
            _mapper = mapper;
        }

        public async Task<Dictionary<Station, List<Playlist>>> ExtractPlaylists(Rule rule)
        {
            var stationPlaylistsKeyValue = new Dictionary<Station, List<Playlist>>();

            var targetStations = rule.Stations;

            if (targetStations == null) return null;

            foreach(var targetStation in targetStations)
            {
                var stationPlaylists = _mapper.Map<List<Playlist>>(await _playlistService.GetPlaylistsByStationAsync(targetStation.Sid));

                bool ruleMatched = rule.SourcePlaylists.All(x => stationPlaylists.Any(y => y.Plid == x.Plid));

                if(ruleMatched)
                    stationPlaylistsKeyValue.Add(targetStation, _mapper.Map<List<Playlist>>(rule.TargetPlaylists));

            }

            return stationPlaylistsKeyValue;
        }
    }
}
