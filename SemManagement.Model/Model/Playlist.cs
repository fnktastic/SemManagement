using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.SemContext.Model
{
    public class Playlist
    {
        [Key]
        public int Plid { get; set; }
        public string Name { get; set; }
        public int Changed { get; set; }
        public DateTime Last_Update_Date { get; set; }
    }
}
