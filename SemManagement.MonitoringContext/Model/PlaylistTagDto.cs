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
        [ForeignKey("Playlist")]
        public int Plid { get; set; }
        public PlaylistDto Playlist { get; set; }

        [ForeignKey("Tag")]
        public Guid TagId { get; set; }
        public TagDto Tag { get; set; }
    }
}
