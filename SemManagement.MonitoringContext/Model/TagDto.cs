using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemManagement.MonitoringContext.Model
{
    [Table("Tag")]
    public class TagDto
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual Collection<PlaylistTagDto> PlaylistTags { get; set; }

        public virtual Collection<StationTagDto> StationTags { get; set; }
    }
}
