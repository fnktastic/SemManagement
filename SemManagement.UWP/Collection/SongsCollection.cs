using SemManagement.UWP.Model;
using SemManagement.UWP.Model.Local.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Collection
{

    public class SongsCollection : ObservableCollection<Song>
    {
        public SongsCollection(IList<Song> songs) : base(songs)
        {
            CollectionChanged += SongsCollection_Changed;

            RecalculateIndexes();
        }

        private static object _locker = new object();
        private void RecalculateIndexes()
        {
            lock (_locker)
            {
                int count = this.Count;

                {
                    for (int i = count - 1; i >= 0; i--)
                    {
                        var item = this[i];

                        item.No = count;

                        count--;
                    }
                }
            }
        }

        private void SongsCollection_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        ~SongsCollection()
        {
            CollectionChanged -= SongsCollection_Changed;
        }
    }
}
