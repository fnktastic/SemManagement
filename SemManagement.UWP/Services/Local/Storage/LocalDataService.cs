using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SemManagement.Local.Storage.Repository;
using SemManagement.UWP.Model.Local.Storage;

namespace SemManagement.UWP.Services.Local.Storage
{
    public interface ILocalDataService
    {
        Task<List<Model.Local.Storage.Rule>> GetAllRulesAsync();
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

        public async Task<List<Model.Local.Storage.Rule>> GetAllRulesAsync()
        {
            var rules = await _rulesRepository.GetAllAsync();

            return _mapper.Map<List<Model.Local.Storage.Rule>>(rules);
        }
    }
}
