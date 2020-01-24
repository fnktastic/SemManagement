using SemManagement.UWP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SemManagement.UWP.Helper
{
    public class SongsComparer : IComparer<Song>
    {
        public int Compare(Song o1, Song o2)
        {
            string x = ((Song)o1).Artist;
            string y = ((Song)o2).Artist;
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
