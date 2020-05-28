using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesProvider.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace GamesProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private IPlatformService _platformService;

        public PlatformsController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [HttpGet()]
        public IActionResult GetPlatforms()
        {
            return Ok(_platformService.GetPlatforms());
        }
    }
}