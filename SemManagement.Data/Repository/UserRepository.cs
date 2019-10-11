using MySql.Data.MySqlClient;
using SemManagement.Data.DataAccess;
using SemManagement.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Data.Repository
{
    public interface IUserRepository
    {
        IList<User> GetTop(int top);
    }

    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public IList<User> GetTop(int top)
        {
            var mySqlParameter = new MySqlParameter("top", MySqlDbType.Int32)
            {
                Value = top
            };

            return _context.Users.SqlQuery("SELECT * FROM Users LIMIT @top", mySqlParameter).ToList();

        }
    }
}
