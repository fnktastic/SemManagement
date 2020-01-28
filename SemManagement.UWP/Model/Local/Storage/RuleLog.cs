using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model.Local.Storage
{
    public class RuleLog
    {
        public Guid Id { get; set; }
        public Guid RuleId { get; set; }
        public Rule Rule { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual Collection<Station> FiredStations { get; set; }
    }
}
