using SemManagement.MonitoringContext.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Model
{
    [Table("Rule")]
    public class RuleDto
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public bool IsRepeat { get; set; }

        public bool IsDraft { get; set; }

        public bool AllStations { get; set; }

        public virtual Collection<RulePlaylistDto> RulePlaylists { get; set; }

        public virtual Collection<RuleStationDto> RuleStations { get; set; }

        public RuleViewModel ToRuleViewModel()
        {
            return new RuleViewModel()
            {
                Id = this.Id,
                Name = this.Name,
                Created = this.Created,
                IsRepeat = this.IsRepeat,
                IsDraft = this.IsDraft,
                AllStations = this.AllStations,
            };
        }
    }
}
