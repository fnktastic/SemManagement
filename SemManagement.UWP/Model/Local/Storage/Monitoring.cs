using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model.Local.Storage
{
    public class Monitoring
    {
        public Guid Id { get; set; }
        public int WantedAmountOfUpdates { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; }
        public int RepeatInterval { get; set; }
    }
}
