using GamesProvider.Services;
using GamesProvider.Services.DTOs;
using GamesProvider.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GamesProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesPricesController : ControllerBase
    {
        private IGamesPricesService _gamesPricesService;

        public GamesPricesController(IGamesPricesService gamesPricesService)
        {
            _gamesPricesService = gamesPricesService;
        }

        [HttpGet("best/{count}")]
        public IActionResult GetBestGamesByCount(int count)
        {
            return Ok(_gamesPricesService.GetBestGames(count));
        }

        [HttpGet("")]
        public IActionResult GetByFilter([FromQuery]FilterRequestDTO filterRequest)
        {
            var games = _gamesPricesService.GetByFilter(filterRequest).ToList();
            var count = _gamesPricesService.GetByFilterCount(filterRequest);
            return Ok(new { count, games });
        }
    }
}
