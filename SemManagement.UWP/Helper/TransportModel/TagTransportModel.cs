using SemManagement.UWP.Model.Local.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Helper.TransportModel
{
    public class TagTransportModel
    {
        public Model.Station Station { get; set; }
        public List<Tag> Tags { get; set; }

        public TagTransportModel()
        {

        }

        public TagTransportModel(Model.Station station, List<Tag> tags)
        {
            Station = station;
            Tags = tags;
        }
    }
}
