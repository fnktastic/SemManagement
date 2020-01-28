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
    [Table("Playlist")]
    public class PlaylistDto
    {
        [Key]
        public int Plid { get; set; }
        public string Name { get; set; }
        public int Changed { get; set; }

        public virtual Collection<PlaylistTagDto> PlaylistTags { get; set; }

        public virtual Collection<RulePlaylistDto> RulePlaylists { get; set; }

        public virtual Collection<StationPlaylistDto> StationPlaylists { get; set; }
    }
}
