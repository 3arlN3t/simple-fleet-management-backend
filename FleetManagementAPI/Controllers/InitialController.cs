using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FleetManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InitialController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "Hello World! It's running!";
        }
    }
}
