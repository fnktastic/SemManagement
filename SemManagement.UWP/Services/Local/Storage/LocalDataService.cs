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
        private readonly IMapper _mapper;
        public LocalDataService(IRulesRepository rulesRepository, IMapper mapper)
        {
            _rulesRepository = rulesRepository;
            _mapper = mapper;
        }

        public async Task<List<Rule>> GetAllRulesAsync()
        {
            var rules = await _rulesRepository.GetAllAsync();

            var mappedRules = _mapper.Map<List<Model.Local.Storage.Rule>>(rules);

            foreach(var mappedRule in mappedRules)
            {
                var rule = rules.First(x => x.Id == mappedRule.Id);

                mappedRule.SourcePlaylists = _mapper.Map<List<Playlist>>(rule.Playlists.Where(x => x.RulePlaylistType == RulePlaylistTypeEnum.Source));

                mappedRule.TargetPlaylists = _mapper.Map<List<Playlist>>(rule.Playlists.Where(x => x.RulePlaylistType == RulePlaylistTypeEnum.Target));
            }

            return mappedRules;
        }

        public async Task SaveRuleAsync(Rule rule)
        {
            var playlists = new List<PlaylistDto>();

            var mappedSorcePlaylists = _mapper.Map<List<PlaylistDto>>(rule.SourcePlaylists);

            mappedSorcePlaylists.ForEach(x => x.RulePlaylistType = RulePlaylistTypeEnum.Source);

            playlists.AddRange(mappedSorcePlaylists);

            var mappedTargetPlaylists = _mapper.Map<List<PlaylistDto>>(rule.TargetPlaylists);

            mappedTargetPlaylists.ForEach(x => x.RulePlaylistType = RulePlaylistTypeEnum.Target);

            playlists.AddRange(mappedTargetPlaylists);

            var mappedRule = _mapper.Map<RuleDto>(rule);

            mappedRule.Playlists = playlists;

            await _rulesRepository.SaveAsync(mappedRule);
        }
    }
}
