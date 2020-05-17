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
        [HttpGet("platform")]
        public IActionResult GetByPlatform(int platformId, int count, int offset)
        {
            return Ok(_gamesPricesService.GetGamePricesByPlatform(platformId, count, offset));
        }
        [HttpGet("platformcount")]
        public IActionResult GetByPlatformCount(int platformId)
        {
            return Ok(_gamesPricesService.GetGamePricesByPlatformCount(platformId));
        }
        [HttpGet("name")]
        public IActionResult GetByName(string name, int count, int offset)
        {
            return Ok(_gamesPricesService.GetGamePricesByName(name, count, offset));
        }
        [HttpGet("namecount")]
        public IActionResult GetByNameCount(string name)
        {
            return Ok(_gamesPricesService.GetGamePricesByNameCount(name));
        }
    }
}
