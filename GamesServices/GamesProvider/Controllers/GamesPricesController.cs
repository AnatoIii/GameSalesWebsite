using GamesProvider.Services;
using GamesProvider.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

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

        [HttpGet("best")]
        public IActionResult GetByFilter(int count)
        {
            return Ok(_gamesPricesService.GetBestGames(count));
        }

        [HttpPost("filtered")]
        public IActionResult GetByFilter([FromBody] FilterRequestDTO filterRequest)
        {
            return Ok(_gamesPricesService.GetByFilter(filterRequest));
        }

        [HttpPost("filteredCount")]
        public IActionResult GetByFilterCount([FromBody] FilterRequestDTO filterRequest)
        {
            return Ok(_gamesPricesService.GetByFilterCount(filterRequest));
        }
    }
}
