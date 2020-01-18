using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Model
{
    [Table("StationPlaylist")]
    public class StationPlaylistDto
    {
        [Key]
        public int StationId { get; set; }
        public StationDto Station { get; set; }

        [Key]
        public int PlaylistId { get; set; }
        public PlaylistDto Playlist { get; set; }
    }
}
