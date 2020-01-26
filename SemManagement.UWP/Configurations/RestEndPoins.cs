using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Configurations
{
    public interface IRestEndpoints
    {
        string Songs { get; }
        string Stations { get; }
        string Users { get; }
        string Playlists { get; }
        string Monitoring { get; }
    }
    public class RestEndpoints : IRestEndpoints
    {

        public string Users => "api/users";

        public string Songs => "api/songs";

        public string Stations => "api/stations";

        public string Playlists => "api/playlists";

        public string Monitoring => "api/monitoring";
    }
}
