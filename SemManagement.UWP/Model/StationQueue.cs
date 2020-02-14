using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model
{
    public class StationQueue : Song
    {
        public string creation_Date { get; set; }
        public int Position { get; set; }
        public int Semid { get; set; }
    }
}
