﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Model
{
    [Table("RuleStation")]
    public class RuleStationDto
    {
        [Key]
        public Guid RuleStationId { get; set; }

        public Guid RuleId { get; set; }
        public RuleDto Rule { get; set; }

        [Key]
        public int StationId { get; set; }
        public StationDto Station { get; set; }
    }
}
