using SemManagement.MonitoringContext.Enum;
using SemManagement.MonitoringContext.Model;
using SemManagement.MonitoringContext.Repository;
using SemManagement.MonitoringContext.Scheduler;
using SemManagement.MonitoringContext.Scheduler.Jobs;
using SemManagement.MonitoringContext.ViewModel;
using SemManagement.SemContext.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Services
{
    public interface IRuleService
    {
        Task SaveRuleAsync(RuleViewModel rule);
        Task<List<RuleViewModel>> GetAllRulesAsync();
        Task FireRule(Guid ruleId);
        Task<List<RuleLogDto>> GetRuleLogs(Guid ruleId);
    }

    public class RuleService : IRuleService
    {
        private readonly ILocalRulesRepository _localRulesRepository;
        private readonly ILocalPlaylistRepository _localPlaylistRepository;
        private readonly ILocalStationRepository _stationRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IMonitoringScheduler _monitoringScheduler;

        public RuleService(IMonitoringScheduler monitoringScheduler, ILocalRulesRepository rulesRepository, ILocalPlaylistRepository localPlaylistRepository, ILocalStationRepository stationRepository, IPlaylistRepository playlistRepository)
        {
            _monitoringScheduler = monitoringScheduler;
            _localRulesRepository = rulesRepository;
            _localPlaylistRepository = localPlaylistRepository;
            _stationRepository = stationRepository;
            _playlistRepository = playlistRepository;
        }

        public async Task<List<RuleViewModel>> GetAllRulesAsync()
        {
            var rules = await _localRulesRepository.GetAllAsync();

            var ruleViewModels = new List<RuleViewModel>();

            foreach (var rule in rules)
            {
                var ruleViewModel = rule.ToRuleViewModel();

                ruleViewModel.SourcePlaylists = rule.RulePlaylists.Where(x => x.RulePlaylistType == RulePlaylistTypeEnum.Source).Select(y =>
                {
                    return y.Playlist;
                }).ToCollection();

                ruleViewModel.TargetPlaylists = rule.RulePlaylists.Where(x => x.RulePlaylistType == RulePlaylistTypeEnum.Target).Select(y =>
                {
                    return y.Playlist;
                }).ToCollection();

                ruleViewModel.Stations = rule.RuleStations.Select(x =>
                {
                    return x.Station;
                }).ToCollection();

                ruleViewModels.Add(ruleViewModel);
            }

            return ruleViewModels;
        }

        public async Task SaveRuleAsync(RuleViewModel rule)
        {
            var savedRule = await _localRulesRepository.SaveAsync(rule.ToRuleDto());

            await _stationRepository.SaveRangeAsync(rule.Stations.ToList());
            await _localPlaylistRepository.SaveRangeAsync(rule.SourcePlaylists.ToList());
            await _localPlaylistRepository.SaveRangeAsync(rule.TargetPlaylists.ToList());

            var sourcePlaylists = rule.SourcePlaylists.Select(x => new RulePlaylistDto()
            {
                PlaylistId = x.Plid,
                RuleId = savedRule.Id,
                RulePlaylistType = RulePlaylistTypeEnum.Source
            }).ToList();

            await _localRulesRepository.AddRulePlaylistRangeAsync(sourcePlaylists);

            var targetPlaylists = rule.TargetPlaylists.Select(x => new RulePlaylistDto()
            {
                PlaylistId = x.Plid,
                RuleId = savedRule.Id,
                RulePlaylistType = RulePlaylistTypeEnum.Target
            }).ToList();

            await _localRulesRepository.AddRulePlaylistRangeAsync(targetPlaylists);

            var stations = rule.Stations.Select(x => new RuleStationDto()
            {
                StationId = x.Sid,
                RuleId = savedRule.Id,
            }).ToList();

            await _localRulesRepository.AddRuleStationRangeAsync(stations);

            if (rule.IsRepeat)
                _monitoringScheduler.AddContiniousJob<SetUpRuleJob>(
                   string.Format("rules_{0}", rule.Id),
                   "rules",
                   rule.Id.ToString()
                );
        }

        public async Task FireRule(Guid ruleId)
        {
            var rule = await _localRulesRepository.GetAsync(ruleId);

            var stationPlaylistsExtractedKeyValue = await ExtractPlaylists(rule);

            var ruleLog = new RuleLogDto()
            {
                Id = Guid.NewGuid(),
                RuleId = rule.Id,
                Timestamp = DateTime.UtcNow,
            };

            var ruleLogStations = new Collection<RuleLogStationDto>();

            foreach (var station in stationPlaylistsExtractedKeyValue.Keys)
            {
                foreach (var playlist in stationPlaylistsExtractedKeyValue[station])
                {
                    bool exists = await _playlistRepository.CheckIfPlaylistAssignedToStation(playlist.Plid, station.Sid);

                    if (exists == false)
                        await _playlistRepository.AddPlaylistToStationAsync(playlist.Plid, station.Sid);
                }

                ruleLogStations.Add(new RuleLogStationDto()
                {
                    RuleLogId = ruleLog.Id,
                    StationSid = station.Sid,
                });
            }

            await _localRulesRepository.AddRuleLog(ruleLog, ruleLogStations);
        }

        private async Task<Dictionary<StationDto, List<PlaylistDto>>> ExtractPlaylists(RuleDto rule)
        {
            var stationPlaylistsKeyValue = new Dictionary<StationDto, List<PlaylistDto>>();

            var targetStations = rule.RuleStations.Select(x => x.Station).ToList(); //need to calculate them

            var sourcePlaylists = rule.RulePlaylists.Where(x => x.RulePlaylistType == RulePlaylistTypeEnum.Source).Select(y => y.Playlist).ToList();

            var targetPlaylists = rule.RulePlaylists.Where(x => x.RulePlaylistType == RulePlaylistTypeEnum.Target).Select(y => y.Playlist).ToList();

            if (targetStations == null) return null;

            foreach (var targetStation in targetStations)
            {
                var stationPlaylists = await _playlistRepository.GetPlaylistsByStationAsync(targetStation.Sid);

                bool ruleMatched = targetPlaylists.All(x => stationPlaylists.Any(y => y.Plid == x.Plid));

                if (ruleMatched)
                    stationPlaylistsKeyValue.Add(targetStation, sourcePlaylists);
            }

            return stationPlaylistsKeyValue;
        }

        public async Task<List<RuleLogDto>> GetRuleLogs(Guid ruleId)
        {
            return await _localRulesRepository.GetRuleLogs(ruleId);
        }
    }
}
