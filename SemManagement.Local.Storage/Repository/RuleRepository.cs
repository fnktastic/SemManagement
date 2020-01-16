﻿using Microsoft.EntityFrameworkCore;
using SemManagement.Local.Storage.DataAccess;
using SemManagement.Local.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Repository
{
    public interface IRulesRepository
    {
        Task<List<Model.RuleDto>> GetAllAsync();

        Task SaveAsync(Model.RuleDto rule);
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
                .Include(x => x.Playlists)
                .Include(x => x.Stations)
                .ToListAsync();
        }

        public async Task SaveAsync(RuleDto rule)
        {
            _context.Rules.Add(rule);

            await _context.SaveChangesAsync();
        }
    }
}
