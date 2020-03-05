using SemManagement.UWP.Configurations;
using SemManagement.UWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.UserModule.Provide
{
    public interface IUserProvider
    {
        Task<List<User>> GetAsync(int? uid);
    }

    public class UserProvider : WebApiProvider, IUserProvider
    {
        public UserProvider(IRestEndpoints restEndpoints, PublicApiConfiguration settings) : base(restEndpoints, settings)
        {
        }

        public Task<List<User>> GetAsync(int? uid)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Users, "take");

            return GetUsersAsync<User>(endpoint, uid);
        }
    }
}
