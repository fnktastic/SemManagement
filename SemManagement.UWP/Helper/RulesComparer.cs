using SemManagement.UWP.Model.Local.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Helper
{
    public class RulesComparer : IComparer<Rule>
    {
        public int Compare(Rule o1, Rule o2)
        {
            string x = ((Rule)o1).Name;
            string y = ((Rule)o2).Name;
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
