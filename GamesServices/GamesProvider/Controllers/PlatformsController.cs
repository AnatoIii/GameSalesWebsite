using GamesProvider.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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