using GamesProvider.Services;
using GamesProvider.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("filtered")]
        public IActionResult GetByFilter([FromQuery]FilterRequestDTO filterRequest)
        {
            var result = _gamesPricesService.GetByFilter(filterRequest).ToList();
            return Ok(result);
        }

        [HttpGet("filteredCount")]
        public IActionResult GetByFilterCount([FromQuery]FilterRequestDTO filterRequest)
        {
            return Ok(_gamesPricesService.GetByFilterCount(filterRequest));
        }
    }
}
