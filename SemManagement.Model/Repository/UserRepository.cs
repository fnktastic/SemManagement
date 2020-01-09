using SemManagement.Model.DataAccess;
using SemManagement.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemManagement.Model.Repository
{
    public interface IUserRepository
    {
        IList<User> GetTopAsync(int top);
    }

    public class UserRepository : IUserRepository
    {
        private readonly SemContext _context;

        public UserRepository(SemContext context)
        {
            _context = context;
        }

        public IList<User> GetTopAsync(int top)
        {
            return _context.Users.Take(top).ToList();
        }
    }
}
