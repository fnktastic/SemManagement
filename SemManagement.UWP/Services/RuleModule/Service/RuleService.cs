using SemManagement.UWP.Configurations;
using SemManagement.UWP.Model.Local.Storage;
using SemManagement.UWP.Services.RuleModule.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.RuleModule.Service
{
    public interface IRuleService
    {
        Task<List<Rule>> GetAllRulesAsync();
        Task SaveRuleAsync(Rule rule);
        Task FireRule(Guid ruleId);
        Task<List<RuleLog>> GetRuleLogs(Guid ruleId);
    }

    public class RuleService : IRuleService
    {
        private readonly IRuleProvider _ruleProvider;

        public RuleService(IRuleProvider ruleProvider)
        {
            _ruleProvider = ruleProvider;
        }

        public Task<List<Rule>> GetAllRulesAsync()
        {
            return _ruleProvider.GetAllRulesAsync();
        }

        public Task SaveRuleAsync(Rule rule)
        {
            return _ruleProvider.SaveRuleAsync(rule);
        }

        public Task<List<RuleLog>> GetRuleLogs(Guid ruleId)
        {
            return _ruleProvider.GetRuleLogs(ruleId);
        }

        public Task FireRule(Guid ruleId)
        {
            return _ruleProvider.FireRule(ruleId);
        }
    }
}
