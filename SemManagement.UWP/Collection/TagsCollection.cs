using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemManagement.UWP.Model.Local.Storage;

namespace SemManagement.UWP.Collection
{
    public class TagsCollection : ObservableCollection<Tag>
    {
        public TagsCollection(IList<Tag> tags) : base(tags)
        {

        }

        public void AddRange(IList<Tag> tags)
        {
            foreach (var tag in tags)
            {
                var item = this.FirstOrDefault(x => string.Equals(x.Name, tag.Name, StringComparison.OrdinalIgnoreCase));

                if (item == null)
                    Add(tag);
            }
        }
    }
}
