using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Model
{
    [Table("Playlist")]
    public class PlaylistDto
    {
        [Key]
        public Guid Id { get; set; }
    }
}
