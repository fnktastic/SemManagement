using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SemManagement.UWP.Model;
using SemManagement.UWP.Model.Local.Storage;
using SemManagement.UWP.Services.Monitoring.Service;
using SemManagement.UWP.Services.RuleModule.Service;
using SemManagement.UWP.Services.StationModule.Service;
using SemManagement.UWP.Services.TagModule.Service;
using SemManagement.UWP.Services.UserModule.Service;
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
        Task SavePlaylistTagRangeAsync(Model.Playlist playlist, List<Tag> tags);
        Task<List<Tag>> GetAllTagsAsync(int sid);
        Task<List<Tag>> GetAllPlaylisTagsAsync(int plid);
        Task<List<Model.Station>> GetStationByTagsAsync(List<Tag> tags);
        Task<List<Model.Playlist>> GetPlaylistsByTagsAsync(List<Tag> tags);
        Task<List<RuleLog>> GetRuleLogs(Guid ruleId);
        Task FireRule(Guid ruleId);
        Task<BoolResult> DeleteStationTagByIdAsync(int stationId, Guid tagId);
        Task<BoolResult> DeletePlaylistTagByIdAsync(int playlistId, Guid tagId);
        Task<List<Model.Station>> GetStationsByPlaylist(int plid);
        Task<FeedList> GetQucikMonitoring(DateTime dateTime);
        Task<List<User>> GetUsersAsync(int? uid);
    }

    public class LocalDataService : ILocalDataService
    {
        private readonly IMapper _mapper;
        private readonly IRuleService _ruleService;
        private readonly ITagService _tagService;
        private readonly IStationService _stationService;
        private readonly IMonitoringService _monitoringService;
        private readonly IUserService _userService;

        public LocalDataService(IMapper mapper, IRuleService ruleService, ITagService tagService, IStationService stationService, IMonitoringService monitoringService, IUserService userService)
        {
            _mapper = mapper;
            _ruleService = ruleService;
            _tagService = tagService;
            _stationService = stationService;
            _monitoringService = monitoringService;
            _userService = userService;
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

        public async Task<List<Model.Playlist>> GetPlaylistsByTagsAsync(List<Tag> tags)
        {
            return await _tagService.GetPlaylistByTagsAsync(tags);
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

        public Task<List<Tag>> GetAllPlaylisTagsAsync(int plid)
        {
            return _tagService.GetAllPlaylistTagsAsync(plid);
        }

        public Task<BoolResult> DeletePlaylistTagByIdAsync(int playlistId, Guid tagId)
        {
            return _tagService.DeletePlaylistTagByIdAsync(playlistId, tagId);
        }

        public Task SavePlaylistTagRangeAsync(Model.Playlist playlist, List<Tag> tags)
        {
            return _tagService.SavePlaylistTagRangeAsync(playlist, tags);
        }

        public Task<List<Model.Station>> GetStationsByPlaylist(int plid)
        {
            return _stationService.GetStationsByPlaylist(plid);
        }

        public Task<FeedList> GetQucikMonitoring(DateTime dateTime)
        {
            return _monitoringService.GetQucikMonitoring(dateTime);
        }

        public Task<List<User>> GetUsersAsync(int? uid)
        {
            return _userService.GetAsync(uid);
        }
    }
}
