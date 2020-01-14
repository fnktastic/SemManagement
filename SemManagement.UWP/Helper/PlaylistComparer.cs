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
    public class PlaylistsComparer : IComparer<Playlist>
    {
        private static readonly Regex numTextSplitRegex = new Regex(@"(?<=\D)(?=\d)|(?<=\d)(?=\D)", RegexOptions.Compiled);

        public int Compare(Playlist o1, Playlist o2)
        {
            string x = ((Playlist)o1).Name;
            string y = ((Playlist)o2).Name;
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
