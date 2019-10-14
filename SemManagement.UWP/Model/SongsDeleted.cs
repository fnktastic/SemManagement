using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model
{
    public class SongsDeleted
    {
        public int Sgid { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Filename { get; set; }
        public string File_MD5 { get; set; }
        public string File_MD5_Gained { get; set; }
        public int Uploaded { get; set; }

        public int Plid { get; set; }
        public string Name { get; set; }
        public int Changed { get; set; }
    }
}
