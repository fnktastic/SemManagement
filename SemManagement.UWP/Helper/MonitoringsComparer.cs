using SemManagement.UWP.Model.Local.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Helper
{
    public class MonitoringsComparer : IComparer<Monitoring>
    {
        public int Compare(Monitoring o1, Monitoring o2)
        {
            string x = ((Monitoring)o1).StationName;
            string y = ((Monitoring)o2).StationName;
            x = x ?? "";
            y = y ?? "";

            char cx = x[0];
            char cy = y[0];

            if (cx > cy) return 1;
            if (cx < cy) return -1;

            return 0;
        }
    }
}
