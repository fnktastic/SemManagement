using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model.Local.Storage
{
    public class Rule
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public bool IsDraft { get; set; }

        public bool IsRepeat { get; set; }

        public ICollection<Playlist> SourcePlaylists { get; set; }

        public ICollection<Playlist> TargetPlaylists { get; set; }

        public ICollection<Station> Stations { get; set; }
    }
}
