using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.SemContext.Model
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

        public byte Blocked { get; set; }

        public byte Soft_Installed { get; set; }

        public byte Synchronized { get; set; }

        public byte Autosync { get; set; }

        public DateTime Last_Update_Date { get; set; }
    }
}
