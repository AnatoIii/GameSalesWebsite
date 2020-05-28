using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesProvider.Services;
using Microsoft.AspNetCore.Http;
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