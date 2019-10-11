using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.Model.Model
{
    public class Song
    {
        [Key]
        public int Sgid { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
    }
}
