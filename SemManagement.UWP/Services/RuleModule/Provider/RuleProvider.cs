using SemManagement.UWP.Configurations;
using SemManagement.UWP.Model.Local.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.RuleModule.Provider
{
    public interface IRuleProvider
    {
        Task<List<Rule>> GetAllRulesAsync();
        Task SaveRuleAsync(Rule rule);
        Task<List<RuleLog>> GetRuleLogs(Guid ruleId);
        Task FireRule(Guid ruleId);

    }

    public class RuleProvider : WebApiProvider, IRuleProvider
    {
        public RuleProvider(IRestEndpoints restEndpoints, PublicApiConfiguration settings) : base(restEndpoints, settings)
        {

        }

        public Task FireRule(Guid ruleId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Rule, "fireRule");

            return FireRule(endpoint, ruleId);
        }

        public Task<List<Rule>> GetAllRulesAsync()
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Rule, "getAllRules");

            return GetAllRulesAsync<Rule>(endpoint);
        }

        public Task<List<RuleLog>> GetRuleLogs(Guid ruleId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Rule, "getRuleLogs");

            return GetRuleLogs<RuleLog>(endpoint, ruleId);
        }

        public Task SaveRuleAsync(Rule rule)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Rule, "saveRule");

            return AddAsync<Rule>(endpoint , rule);
        }
    }
}
