using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Model
{
    [Table("StationTag")]
    public class StationTagDto
    {
        [Key]
        public int StationId { get; set; }
        public StationDto Station { get; set; }

        [Key]
        public Guid TagId { get; set; }
        public TagDto Tag { get; set; }
    }
}
