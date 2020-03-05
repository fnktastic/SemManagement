using Microsoft.AspNetCore.Mvc;
using SemManagement.SemContext.Model;
using SemManagement.SemContext.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("take")]
        public ActionResult<List<User>> Index([FromQuery] int? uid)
        {
            if(uid.HasValue && uid != 0)
            {
                return _userRepository.GetAsync(uid.Value).ToList();
            }

            return _userRepository.GetAsync().ToList();
        }
    }
}
