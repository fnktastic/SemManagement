using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SemManagement.Local.Storage.Enums;
using SemManagement.Local.Storage.Model;
using SemManagement.Local.Storage.Repository;
using SemManagement.UWP.Model.Local.Storage;
using SemManagement.UWP.ViewModel;
using SemManagement.UWP.ViewModel.ContentDialog;
using Rule = SemManagement.UWP.Model.Local.Storage.Rule;

namespace SemManagement.UWP.Services.Local.Storage
{
    public interface ILocalDataService
    {
        Task<List<Rule>> GetAllRulesAsync();
        Task SaveRuleAsync(Rule rule);
    }

    public class LocalDataService : ILocalDataService
    {
        private readonly IRulesRepository _rulesRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IStationRepository _stationRepository;
        private readonly IMapper _mapper;
        public LocalDataService(IRulesRepository rulesRepository, IMapper mapper, IPlaylistRepository playlistRepository, IStationRepository stationRepository)
        {
            _rulesRepository = rulesRepository;
            _mapper = mapper;
            _playlistRepository = playlistRepository;
            _stationRepository = stationRepository;
        }

        public async Task<List<Rule>> GetAllRulesAsync()
        {
            var rules = await _rulesRepository.GetAllAsync();

            var mappedRules = _mapper.Map<List<Model.Local.Storage.Rule>>(rules);

            foreach(var mappedRule in mappedRules)
            {
                var rule = rules.First(x => x.Id == mappedRule.Id);

                mappedRule.SourcePlaylists = _mapper.Map<List<Playlist>>(rule.RulePlaylists.Where(x => x.RulePlaylistType == RulePlaylistTypeEnum.Source).Select(y => y.Playlist));

                mappedRule.TargetPlaylists = _mapper.Map<List<Playlist>>(rule.RulePlaylists.Where(x => x.RulePlaylistType == RulePlaylistTypeEnum.Target).Select(y => y.Playlist));

                mappedRule.Stations = _mapper.Map<List<Station>>(rule.RuleStations.Select(x => x.Station));
            }

            return mappedRules;
        }

        public async Task SaveRuleAsync(Rule rule)
        {
            var savedRule = await _rulesRepository.SaveAsync(_mapper.Map<RuleDto>(rule));

            await _stationRepository.SaveRangeAsync(_mapper.Map<List<StationDto>>(rule.Stations));
            await _playlistRepository.SaveRangeAsync(_mapper.Map<List<PlaylistDto>>(rule.SourcePlaylists));
            await _playlistRepository.SaveRangeAsync(_mapper.Map<List<PlaylistDto>>(rule.TargetPlaylists));

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
