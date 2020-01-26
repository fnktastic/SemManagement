﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model
{
    public class Monitoring
    {
        [Key]
        public Guid Id { get; set; }
        public int WantedAmountOfUpdates { get; set; }
        public int StationId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public int RepeatInterval { get; set; }
    }
}