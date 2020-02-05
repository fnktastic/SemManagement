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
        Task FireRule(Guid ruleId, DateTime? dateTime = null);
        Task<List<RuleLogDto>> GetRuleLogs(Guid ruleId);
        Task FireRules();
    }

    public class RuleService : IRuleService
    {
        private readonly ILocalRulesRepository _localRulesRepository;
        private readonly ILocalPlaylistRepository _localPlaylistRepository;
        private readonly ILocalStationRepository _stationRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ISnapshotEntryRepository _snapshotEntryRepository;

        public RuleService(ISnapshotEntryRepository snapshotEntryRepository, ILocalRulesRepository rulesRepository, ILocalPlaylistRepository localPlaylistRepository, ILocalStationRepository stationRepository, IPlaylistRepository playlistRepository)
        {
            _localRulesRepository = rulesRepository;
            _localPlaylistRepository = localPlaylistRepository;
            _stationRepository = stationRepository;
            _playlistRepository = playlistRepository;
            _snapshotEntryRepository = snapshotEntryRepository;
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
        }

        public async Task FireRules()
        {
            DateTime now = DateTime.UtcNow;

            await _snapshotEntryRepository.InsertAsync(new SnapshotEntryDto(SnapshotTypeEnum.Rule, SnapshotEntryState.Started, now));

            var rules = (await _localRulesRepository.GetAllAsync()).Where(x => x.IsRepeat).ToList();

            foreach(var rule in rules)
            {
                await FireRule(rule.Id, now);
            };

            await _snapshotEntryRepository.InsertAsync(new SnapshotEntryDto(SnapshotTypeEnum.Rule, SnapshotEntryState.Finished, DateTime.UtcNow));
        }

        public async Task FireRule(Guid ruleId, DateTime? dateTime = null)
        {
            DateTime now = DateTime.UtcNow;

            if (dateTime.HasValue == true)
                now = dateTime.Value;

            var rule = await _localRulesRepository.GetAsync(ruleId);

            var stationPlaylistsExtractedKeyValue = await ExtractPlaylists(rule);

            var ruleLogs = new List<RuleLogDto>();

            foreach (var station in stationPlaylistsExtractedKeyValue.Keys)
            {
                foreach (var playlist in stationPlaylistsExtractedKeyValue[station])
                {
                    bool exists = await _playlistRepository.CheckIfPlaylistAssignedToStation(playlist.Plid, station.Sid);

                    if (exists == false)
                    {
                        await _playlistRepository.AddPlaylistToStationAsync(playlist.Plid, station.Sid);

                        var ruleLog = new RuleLogDto()
                        {
                            Id = Guid.NewGuid(),
                            RuleId = rule.Id,
                            Timestamp = now,
                            Message = string.Format("Playlist {0} (ID = {1}) -> Station {2} (ID = {3})", playlist.Name, playlist.Plid, station.Name, station.Sid)
                        };

                        ruleLogs.Add(ruleLog);
                    }
                }
            }

            await _localRulesRepository.AddRuleLog(ruleLogs);
        }

        private async Task<Dictionary<StationDto, List<PlaylistDto>>> ExtractPlaylists(RuleDto rule)
        {
            var stationPlaylistsKeyValue = new Dictionary<StationDto, List<PlaylistDto>>();

            var targetStations = rule.RuleStations.Select(x => x.Station).ToList();

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
