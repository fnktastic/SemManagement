using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.LocalContext.Model
{
    [Table("Tag")]
    public class TagDto
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PlaylistTagDto> PlaylistTags { get; set; }

        public virtual ICollection<StationTagDto> StationTags { get; set; }
    }
}
