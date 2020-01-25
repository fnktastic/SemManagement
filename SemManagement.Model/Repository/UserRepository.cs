using SemManagement.SemContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemManagement.SemContext.Repository
{
    public interface IUserRepository
    {
        IList<User> GetTopAsync(int top);
    }

    public class UserRepository : IUserRepository
    {
        private readonly SemDbContext _context;

        public UserRepository(SemDbContext context)
        {
            _context = context;
        }

        public IList<User> GetTopAsync(int top)
        {
            return _context.Users.Take(top).ToList();
        }
    }
}
