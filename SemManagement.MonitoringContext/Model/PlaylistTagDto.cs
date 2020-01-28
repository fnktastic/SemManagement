using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Model
{
    [Table("PlaylistTag")]
    public class PlaylistTagDto
    {
        [Key]
        public int PlaylistId { get; set; }
        public PlaylistDto Playlist { get; set; }

        [Key]
        public Guid TagId { get; set; }
        public TagDto Tag { get; set; }
    }
}
