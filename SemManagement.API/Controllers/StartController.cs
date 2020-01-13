using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Index()
        {
            return await Task.FromResult<ActionResult<string>>(new ActionResult<string>("SEM Music DB Tool API"));
        }
    }
}
