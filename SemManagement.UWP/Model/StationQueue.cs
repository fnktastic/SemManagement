using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model
{
    public class StationQueue
    {
        public int Sgid { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Filename { get; set; }
        public string file_MD5 { get; set; }
        public string file_MD5_Gained { get; set; }
        public int Uploaded { get; set; }

        public string creation_Date { get; set; }
        public int Position { get; set; }
        public int Semid { get; set; }
    }
}
