using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Data.Model
{
    public class Station
    {
        [Key]
        public int Sid { get; set; }

        public int Uid { get; set; }

        public string Name { get; set; }

        public string Hardware_ID { get; set; }

        public DateTime Licence { get; set; }

        public int Type { get; set; }

        public bool Blocked { get; set; }

        public bool Soft_Installed { get; set; }

        public bool Synchronized { get; set; }

        public bool Autosync { get; set; }
    }
}
