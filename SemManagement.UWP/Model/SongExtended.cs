using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model
{
    public class SongExtended : Song
    {
        public int Plid { get; set; }
        public string Name { get; set; }
        public int Changed { get; set; }
    }
}
