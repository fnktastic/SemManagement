using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Model
{
    public class MonitoringDto
    {
        [Key]
        public Guid Id { get; set; }

        public int StationId { get; set; }
        public StationDto Station { get; set; }

        public DateTime? StartDateTime { get; set; }
        public int RepeatInterval { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
    }
}
