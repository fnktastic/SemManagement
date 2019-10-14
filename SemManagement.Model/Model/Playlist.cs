using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.Model.Model
{
    public class Playlist
    {
        [Key]
        public int Plid { get; set; }
        public string Name { get; set; }
        public int Changed { get; set; }
    }
}
