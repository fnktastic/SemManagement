using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.LocalContext.Model
{
    [Table("Playlist")]
    public class PlaylistDto
    {
        [Key]
        public int Plid { get; set; }
        public string Name { get; set; }
        public int Changed { get; set; }

        public virtual ICollection<PlaylistTagDto> PlaylistTags { get; set; }

        public virtual ICollection<RulePlaylistDto> RulePlaylists { get; set; }

        public virtual ICollection<StationPlaylistDto> StationPlaylists { get; set; }
    }
}
