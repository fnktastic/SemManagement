using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.SemContext.Model
{
    public class Playlistssongs
    {
        public int Plid { get; set; }
        public int Sgid { get; set; }
        public int Position { get; set; }
        [Key]
        public int Psid { get; set; }
        public DateTime Last_Update_Date { get; set; }
    }
}
