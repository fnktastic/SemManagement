using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Model
{
    [Table("Station")]
    public class StationDto
    {
        [Key]
        public Guid Id { get; set; }

        public int Sid { get; set; }

        public int Uid { get; set; }

        public string Name { get; set; }

        public string Hardware_ID { get; set; }

        public DateTime Licence { get; set; }

        public int Type { get; set; }

        public int Blocked { get; set; }

        public int Soft_Installed { get; set; }

        public int Synchronized { get; set; }

        public int Autosync { get; set; }

        public Guid RuleDtoId { get; set; }
        public virtual RuleDto Rule { get; set; }
    }
}
