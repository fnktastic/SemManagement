using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.SemContext.Model
{
    public class Stationsstatus
    {
        [Key]
        public int idStation { get; set; }
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
        public string ChangedDate { get; set; }
        public DateTime Last_Update_Date { get; set; }
    }
}
