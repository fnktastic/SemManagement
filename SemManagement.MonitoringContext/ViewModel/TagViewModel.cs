using SemManagement.MonitoringContext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemManagement.MonitoringContext.ViewModel
{
    public class StationTagViewModel
    {
        public StationDto Station { get; set; }
        public List<TagDto> Tags { get; set; }
    }

    public class PlaylistTagViewModel
    {
        public PlaylistDto Playlist { get; set; }
        public List<TagDto> Tags { get; set; }
    }
}
