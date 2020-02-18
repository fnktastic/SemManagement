using SemManagement.UWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Collection
{
    public class StationsCollection : ObservableCollection<Station>
    {
        public StationsCollection(IList<Station> stations) : base(stations)
        {

        }

        public StationsCollection(IEnumerable<Station> stations) : base(stations)
        {

        }

        public StationsCollection()
        {

        }

        public void AddStation(Station station)
        {
            if (this.Any(x => x.Sid == station.Sid)) return;

            this.Add(station);
        }

        private void StationCollection_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        ~StationsCollection()
        {

        }
    }
}
