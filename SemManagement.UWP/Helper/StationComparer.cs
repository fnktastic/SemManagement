using SemManagement.UWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Helper
{
    public class StationComparer : IComparer<Station>
    {
        public int Compare(Station o1, Station o2)
        {
            string x = ((Station)o1).Name;
            string y = ((Station)o2).Name;
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
