using SemManagement.Local.Storage.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Repository
{
    public interface ITagRepository
    {

    }

    public class TagRepository : ITagRepository
    {
        private LocalStorageContext _context;

        public TagRepository(LocalStorageContext context)
        {
            _context = context;
        }
    }
}
