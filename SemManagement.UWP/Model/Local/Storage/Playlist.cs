using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model.Local.Storage
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public int Plid { get; set; }
        public string Name { get; set; }
        public int Changed { get; set; }
    }
}
