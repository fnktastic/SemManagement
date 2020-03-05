using SemManagement.SemContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemManagement.SemContext.Repository
{
    public interface IUserRepository
    {
        IList<User> GetAsync();
        IList<User> GetAsync(int uid);
    }

    public class UserRepository : IUserRepository
    {
        private readonly SemDbContext _context;

        public UserRepository(SemDbContext context)
        {
            _context = context;
        }

        public IList<User> GetAsync()
        {
            return _context.Users.ToList();
        }

        public IList<User> GetAsync(int uid)
        {
            return _context.Users.Where(x => x.Uid == uid).ToList();
        }
    }
}
