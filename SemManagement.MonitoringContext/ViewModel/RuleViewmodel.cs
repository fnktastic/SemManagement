using SemManagement.MonitoringContext.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SemManagement.MonitoringContext.ViewModel
{
    public class RuleViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public bool IsDraft { get; set; }

        public bool IsRepeat { get; set; }

        public bool AllStations { get; set; }

        public Collection<PlaylistDto> SourcePlaylists { get; set; }

        public Collection<PlaylistDto> TargetPlaylists { get; set; }

        public Collection<StationDto> Stations { get; set; }

        public RuleDto ToRuleDto()
        {
            return new RuleDto()
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
