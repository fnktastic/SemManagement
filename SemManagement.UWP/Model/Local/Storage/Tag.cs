using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model.Local.Storage
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Tag(string name)
        {
            Name = name;
        }

        public Tag()
        {

        }
    }
}
