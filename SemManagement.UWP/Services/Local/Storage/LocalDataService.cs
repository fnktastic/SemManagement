using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SemManagement.UWP.Model.Local.Storage;
using SemManagement.UWP.Services.RuleModule.Service;
using SemManagement.UWP.ViewModel;
using SemManagement.UWP.ViewModel.ContentDialog;
using Rule = SemManagement.UWP.Model.Local.Storage.Rule;

namespace SemManagement.UWP.Services.Local.Storage
{
    public interface ILocalDataService
    {
        Task<List<Rule>> GetAllRulesAsync();
        Task SaveRuleAsync(Rule rule);
        Task SaveStationTagRangeAsync(Model.Station station, IEnumerable<Tag> tags);
        Task<List<Tag>> GetAllTagsAsync(int sid);
        Task<List<Model.Station>> GetStationByTagsAsync(List<Tag> tags);
    }

    public class LocalDataService : ILocalDataService
    {
        private readonly IMapper _mapper;
        private readonly IRuleService _ruleService;
        public LocalDataService(IMapper mapper, IRuleService ruleService)
        {
            _mapper = mapper;
            _ruleService = ruleService;
        }

        public async Task<List<Rule>> GetAllRulesAsync()
        {
            return await _ruleService.GetAllRulesAsync();
        }

        public async Task SaveRuleAsync(Rule rule)
        {
            await _ruleService.SaveRuleAsync(rule);
        }

        public async Task SaveStationTagRangeAsync(Model.Station station, IEnumerable<Tag> tags)
        {

        }

        public async Task<List<Tag>> GetAllTagsAsync(int sid)
        {
            return null;
        }

        public async Task<List<Model.Station>> GetStationByTagsAsync(List<Tag> tags)
        {
            return null;
        }
    }
}
