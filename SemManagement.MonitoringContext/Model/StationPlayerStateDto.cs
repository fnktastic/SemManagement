using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SemManagement.MonitoringContext.Model
{
    [Table("StationPlayerState")]
    public class StationPlayerStateDto
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public int StationId { get; set; }
        public string QueueName { get; set; }
        public string CurrentSongTitle { get; set; }
        public string CurrentSongArtist { get; set; }
        public float Volume { get; set; }
        public float CrossFade { get; set; }
        public int Mute { get; set; }
        public int Loop { get; set; }
        public int Shuffle { get; set; }
        public int Stopped { get; set; }
        public int Playing { get; set; }
        public int Online { get; set; }
        public int schedulerenabled { get; set; }
        public int? CurrentSongSemId { get; set; }
        public DateTime? ChangedDate { get; set; }
    }
}
