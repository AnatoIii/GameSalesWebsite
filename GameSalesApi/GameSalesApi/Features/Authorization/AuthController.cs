using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using GameSalesApi.Features.Authorization.Commands;
using GameSalesApi.Features.Authorization.CommandHandlers;
using Infrastructure.InfrastructureCommandDecorators;
using Infrastructure.Result;
using DataAccess;

namespace GameSalesApi.Features.Authorization
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GameSalesContext _dbContext;
        private readonly ILogger<AuthController> _logger;
        private readonly TokenCreator _tokenCreator;

        public AuthController(GameSalesContext dbContext, ILogger<AuthController> logger, TokenCreator tokenCreator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _tokenCreator = tokenCreator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginCommand loginCommand)
        {
            var handler = new CommandDecoratorBuilder<LoginCommand, Result<TokenDTO>>()
                .Add<LoginCommandHandler>()
                    .AddParameter<GameSalesContext>(_dbContext)
                    .AddParameter<TokenCreator>(_tokenCreator)
                .AddBaseDecorators(_logger, _dbContext)
                .Build();

            var result = handler.Handle(loginCommand);
            if (result.Failure)
                return BadRequest($"{nameof(loginCommand)} failed. Message: {result.Error}");

            return Ok(result.Value);
        }

        [HttpPost("create")]
        public IActionResult CreateNewUser([FromBody] NewUserCommand newUserCommand)
        {
            var handler = new CommandDecoratorBuilder<NewUserCommand, Result>()
                .Add<NewUserCommandHandler>()
                    .AddParameter<GameSalesContext>(_dbContext)
                .AddBaseDecorators(_logger, _dbContext)
                .Build();

            var result = handler.Handle(newUserCommand);

            if (result.Failure)
                return BadRequest($"{nameof(newUserCommand)} failed. Message: {result.Error}");

            return Ok();
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] TokenDTO tokenDTO)
        {
            var handler = new CommandDecoratorBuilder<TokenDTO, Result<TokenDTO>>()
                .Add<RefreshTokenCommandHandler>()
                    .AddParameter<GameSalesContext>(_dbContext)
                    .AddParameter<TokenCreator>(_tokenCreator)
                .AddBaseDecorators(_logger, _dbContext)
                .Build();

            var result = handler.Handle(tokenDTO);
            if (result.Failure)
                return BadRequest($"{nameof(tokenDTO)} failed. Message: {result.Error}");

            return Ok(result.Value);
        }
    }
}