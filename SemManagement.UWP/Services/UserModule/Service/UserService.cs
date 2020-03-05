using SemManagement.UWP.Model;
using SemManagement.UWP.Services.UserModule.Provide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.UserModule.Service
{
    public interface IUserService
    {
        Task<List<User>> GetAsync(int? uid);
    }

    public class UserService : IUserService
    {
        private readonly IUserProvider _userProvider;

        public UserService(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public Task<List<User>> GetAsync(int? uid)
        {
            return _userProvider.GetAsync(uid);
        }
    }
}
