using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Model
{
    [Table("Rule")]
    public class RuleDto
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public bool IsRepeat { get; set; }

        public bool AllStations { get; set; }

        public virtual ICollection<RulePlaylistDto> RulePlaylists { get; set; }

        public virtual ICollection<RuleStationDto> RuleStations { get; set; }
    }
}
