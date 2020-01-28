using SemManagement.UWP.Model.Local.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Helper.TransportModel
{
    public class TagViewModel
    {
        public Model.Station Station { get; set; }
        public List<Tag> Tags { get; set; }

        public TagViewModel()
        {

        }

        public TagViewModel(Model.Station station, List<Tag> tags)
        {
            Station = station;
            Tags = tags;
        }
    }
}
