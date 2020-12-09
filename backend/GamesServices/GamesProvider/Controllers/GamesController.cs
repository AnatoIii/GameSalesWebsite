using GamesProvider.Services;
using GamesProvider.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GamesProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_gameService.GetById(id));
        }
    }
}