using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemManagement.MonitoringContext.Model
{
    [Table("StationPlaylist")]
    public class StationPlaylistDto
    {
        [Key]
        public Guid StationPlaylistId { get; set; }

        public int StationId { get; set; }
        public StationDto Station { get; set; }

        [Key]
        public int PlaylistId { get; set; }
        public PlaylistDto Playlist { get; set; }
    }
}
