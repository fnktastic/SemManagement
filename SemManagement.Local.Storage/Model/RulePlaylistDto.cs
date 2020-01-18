using SemManagement.Local.Storage.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Model
{
    [Table("RulePlaylist")]
    public class RulePlaylistDto
    {
        [Key]
        public Guid RuleId { get; set; }
        public RuleDto Rule { get; set; }

        [Key]
        public int PlaylistId { get; set; }
        public PlaylistDto Playlist { get; set; }
        
        public RulePlaylistTypeEnum RulePlaylistType { get; set; }
    }
}
