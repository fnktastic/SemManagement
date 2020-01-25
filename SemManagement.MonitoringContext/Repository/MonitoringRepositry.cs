using SemManagement.MonitoringContext.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemManagement.MonitoringContext.Repository
{
    public interface IMonitoringRepositry
    {

    }

    public class MonitoringRepositry : IMonitoringRepositry
    {
        private readonly MonitoringDbContext _context;

        public MonitoringRepositry(MonitoringDbContext context)
        {
            _context = context;
        }
    }
}
