using Microsoft.EntityFrameworkCore;
using SemManagement.Local.Storage.DataAccess;
using SemManagement.Local.Storage.Model;
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

        public async Task<List<Model.RuleDto>> GetAllAsync()
        {
            return await _context.Rules
                .Include(x => x.RulePlaylists)
                    .ThenInclude(y => y.Playlist)
                .Include(x => x.RuleStations)
                    .ThenInclude(y => y.Station)
                .ToListAsync();
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
