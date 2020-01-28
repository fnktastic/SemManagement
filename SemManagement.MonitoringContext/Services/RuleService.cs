using SemManagement.MonitoringContext.Enum;
using SemManagement.MonitoringContext.Model;
using SemManagement.MonitoringContext.Repository;
using SemManagement.MonitoringContext.Scheduler;
using SemManagement.MonitoringContext.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Services
{
    public interface IRuleService
    {
        Task SaveRuleAsync(RuleViewModel rule);
        Task<List<RuleViewModel>> GetAllRulesAsync();
    }

    public class RuleService : IRuleService
    {
        private readonly ILocalRulesRepository _rulesRepository;
        private readonly ILocalPlaylistRepository _playlistRepository;
        private readonly ILocalStationRepository _stationRepository;

        public RuleService(ILocalRulesRepository rulesRepository, ILocalPlaylistRepository playlistRepository, ILocalStationRepository stationRepository)
        {
            _rulesRepository = rulesRepository;
            _playlistRepository = playlistRepository;
            _stationRepository = stationRepository;
        }

        public async Task<List<RuleViewModel>> GetAllRulesAsync()
        {
            var rules = await _rulesRepository.GetAllAsync();

            var ruleViewModels = new List<RuleViewModel>();

            foreach (var rule in rules)
            {
                var ruleViewModel = rule.ToRuleViewModel();

                ruleViewModel.SourcePlaylists = rule.RulePlaylists.Where(x => x.RulePlaylistType == RulePlaylistTypeEnum.Source).Select(y => 
                {
                    y.Playlist.RulePlaylists = null;
                    y.Playlist.StationPlaylists = null;
                    y.Rule = null;

                    return y.Playlist;
                }).ToCollection();

                ruleViewModel.TargetPlaylists = rule.RulePlaylists.Where(x => x.RulePlaylistType == RulePlaylistTypeEnum.Target).Select(y =>
                {
                    y.Playlist.RulePlaylists = null;
                    y.Playlist.StationPlaylists = null;
                    y.Rule = null;

                    return y.Playlist;
                }).ToCollection();

                ruleViewModel.Stations = rule.RuleStations.Select(x => 
                {
                    x.Station.StationPlaylists = null;
                    x.Station.StationTags = null;
                    x.Rule = null;

                    return x.Station;
                }).ToCollection();

                ruleViewModels.Add(ruleViewModel);
            }

            return ruleViewModels;
        }

        public async Task SaveRuleAsync(RuleViewModel rule)
        {
            var savedRule = await _rulesRepository.SaveAsync(rule.ToRuleDto());

            await _stationRepository.SaveRangeAsync(rule.Stations.ToList());
            await _playlistRepository.SaveRangeAsync(rule.SourcePlaylists.ToList());
            await _playlistRepository.SaveRangeAsync(rule.TargetPlaylists.ToList());

            var sourcePlaylists = rule.SourcePlaylists.Select(x => new RulePlaylistDto()
            {
                PlaylistId = x.Plid,
                RuleId = savedRule.Id,
                RulePlaylistType = RulePlaylistTypeEnum.Source
            }).ToList();

            await _rulesRepository.AddRulePlaylistRangeAsync(sourcePlaylists);

            var targetPlaylists = rule.TargetPlaylists.Select(x => new RulePlaylistDto()
            {
                PlaylistId = x.Plid,
                RuleId = savedRule.Id,
                RulePlaylistType = RulePlaylistTypeEnum.Target
            }).ToList();

            await _rulesRepository.AddRulePlaylistRangeAsync(targetPlaylists);

            var stations = rule.Stations.Select(x => new RuleStationDto()
            {
                StationId = x.Sid,
                RuleId = savedRule.Id,
            }).ToList();

            await _rulesRepository.AddRuleStationRangeAsync(stations);
        }
    }
}
