using System;
using Infrastructure.CommandBase;
using Infrastructure.InfrastructureCommandDecorators;
using Infrastructure.InfrastructureQueryDecorators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.HandlerBase;

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
            var builder = new CommandDecoratorBuilder<LoginCommand, Result<TokenDTO>>();

            var handler = builder.Add<LoginCommandHandler>().AddParameter<GameSalesContext>(_dbContext)
                                                            .AddParameter<TokenCreator>(_tokenCreator)
                              .Add<ValidationCommandDecorator<LoginCommand, Result<TokenDTO>>>()
                              .Add<LoggerCommandDecorator<LoginCommand, Result<TokenDTO>>>()
                                                            .AddParameter<ILogger<AuthController>>(_logger)
                              .Add<ProfilerCommandDecorator<LoginCommand, Result<TokenDTO>>>()
                              .Add<SaveChangesCommandDecorator<LoginCommand, Result<TokenDTO>>>()
                                                            .AddParameter<ICommandDispatcher>(null)
                                                            .AddParameter<GameSalesContext>(_dbContext)
                              .Build();

            var result = handler.Handle(loginCommand);
            if (result.Failure)
                return BadRequest($"{nameof(loginCommand)} failed. Message: {result.Error}");
            return Ok(result.Value);
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] TokenDTO tokenDTO)
        {
            var handler = new CommandDecoratorBuilder<TokenDTO, Result<TokenDTO>>()
                .Add<RefreshTokenCommandHandler>()
                    .AddParameter<GameSalesContext>(_dbContext)
                    .AddParameter<TokenCreator>(_tokenCreator)
                .Add<ValidationCommandDecorator<TokenDTO, Result<TokenDTO>>>()
                .Add<LoggerCommandDecorator<TokenDTO,Result<TokenDTO>>>()
                    .AddParameter<ILogger<AuthController>>(_logger)
                .Add<ProfilerCommandDecorator<TokenDTO,Result<TokenDTO>>>()
                .Add<SaveChangesCommandDecorator<TokenDTO, Result<TokenDTO>>>()
                    .AddParameter<ICommandDispatcher>(null)
                    .AddParameter<GameSalesContext>(_dbContext)
                .Build();

            var result = handler.Handle(tokenDTO);
            if (result.Failure)
                return BadRequest($"{nameof(tokenDTO)} failed. Message: {result.Error}");

            return Ok(result.Value);
        }
    }
}