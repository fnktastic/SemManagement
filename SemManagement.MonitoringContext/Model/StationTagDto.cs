using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemManagement.MonitoringContext.Model
{
    [Table("StationTag")]
    public class StationTagDto
    {
        public int Sid { get; set; }
        public StationDto Station { get; set; }

        public Guid TagId { get; set; }
        public TagDto Tag { get; set; }
    }
}
