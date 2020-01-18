using Microsoft.EntityFrameworkCore;
using SemManagement.Local.Storage.DataAccess;
using SemManagement.Local.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Repository
{
    public interface IRulesRepository
    {
        Task<List<Model.RuleDto>> GetAllAsync();

        Task<RuleDto> SaveAsync(RuleDto rule);

        Task AddRulePlaylistRangeAsync(List<RulePlaylistDto> rulePlaylists);

        Task AddRuleStationRangeAsync(List<RuleStationDto> ruleStations);
    }

    public class RulesRepository : IRulesRepository
    {
        private LocalStorageContext _context;

        public RulesRepository(LocalStorageContext context)
        {
            _context = context;
        }

        #region private methods
        private async Task FullLoad(Guid ruleId)
        {
            var rule = await _context.Rules.FirstOrDefaultAsync(x => x.Id == ruleId);

            var rulePlaylists = await _context.RulePlaylists.Where(x => x.RuleId == ruleId).Include(x => x.Playlist).ToListAsync();

            var ruleStations = await _context.RuleStations.Where(x => x.RuleId == ruleId).Include(x => x.Station).ToListAsync();

            rule.RulePlaylists = rulePlaylists;

            rule.RuleStations = ruleStations;
        }
        #endregion

        public async Task<List<Model.RuleDto>> GetAllAsync()
        {
            var rules = await _context.Rules.ToListAsync();

            foreach(var rule in rules)
            {
                await FullLoad(rule.Id);
            }

            return rules;
        }

        public async Task<RuleDto> SaveAsync(RuleDto rule)
        {
            var savedRule = _context.Rules.Add(rule);

            await _context.SaveChangesAsync();

            return savedRule.Entity;
        }

        public async Task AddRulePlaylistRangeAsync(List<RulePlaylistDto> rulePlaylists)
        {
            await _context.RulePlaylists.AddRangeAsync(rulePlaylists);

            await _context.SaveChangesAsync();
        }

        public async Task AddRuleStationRangeAsync(List<RuleStationDto> ruleStations)
        {
            await _context.RuleStations.AddRangeAsync(ruleStations);

            await _context.SaveChangesAsync();
        }
    }
}
