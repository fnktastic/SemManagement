using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SemManagement.UWP.Model.Local.Storage;
using SemManagement.UWP.Services.RuleModule.Service;
using SemManagement.UWP.Services.TagModule.Service;
using SemManagement.UWP.ViewModel;
using SemManagement.UWP.ViewModel.ContentDialog;
using Rule = SemManagement.UWP.Model.Local.Storage.Rule;

namespace SemManagement.UWP.Services.Local.Storage
{
    public interface ILocalDataService
    {
        Task<List<Rule>> GetAllRulesAsync();
        Task SaveRuleAsync(Rule rule);
        Task SaveStationTagRangeAsync(Model.Station station, List<Tag> tags);
        Task<List<Tag>> GetAllTagsAsync(int sid);
        Task<List<Model.Station>> GetStationByTagsAsync(List<Tag> tags);
        Task<List<RuleLog>> GetRuleLogs(Guid ruleId);
        Task FireRule(Guid ruleId);
        Task<BoolResult> DeleteStationTagByIdAsync(int stationId, Guid tagId);
    }

    public class LocalDataService : ILocalDataService
    {
        private readonly IMapper _mapper;
        private readonly IRuleService _ruleService;
        private readonly ITagService _tagService;

        public LocalDataService(IMapper mapper, IRuleService ruleService, ITagService tagService)
        {
            _mapper = mapper;
            _ruleService = ruleService;
            _tagService = tagService;
        }

        public async Task<List<Rule>> GetAllRulesAsync()
        {
            return await _ruleService.GetAllRulesAsync();
        }

        public async Task SaveRuleAsync(Rule rule)
        {
            await _ruleService.SaveRuleAsync(rule);
        }

        public async Task SaveStationTagRangeAsync(Model.Station station, List<Tag> tags)
        {
            await _tagService.SaveStationTagRangeAsync(station, tags);
        }

        public async Task<List<Tag>> GetAllTagsAsync(int sid)
        {
            return await _tagService.GetAllTagsAsync(sid);
        }

        public async Task<List<Model.Station>> GetStationByTagsAsync(List<Tag> tags)
        {
            return await _tagService.GetStationByTagsAsync(tags);
        }

        public Task<List<RuleLog>> GetRuleLogs(Guid ruleId)
        {
            return _ruleService.GetRuleLogs(ruleId);
        }

        public Task FireRule(Guid ruleId)
        {
            return _ruleService.FireRule(ruleId);
        }

        public Task<BoolResult> DeleteStationTagByIdAsync(int stationId, Guid tagId)
        {
            return _tagService.DeleteStationTagByIdAsync(stationId, tagId);
        }
    }
}
