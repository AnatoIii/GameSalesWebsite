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

namespace GameSalesApi.Features.Authorization
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GameSalesContext dbContext;
        private readonly ILogger<AuthController> logger;
        private readonly IOptions<TokenConfig> tokenConfig;
        public AuthController(GameSalesContext dbContext, ILogger<AuthController> logger, IOptions<TokenConfig> tokenConfig)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.tokenConfig = tokenConfig;
        }
        [HttpPost("login")]
        public TokenDTO Login([FromBody] LoginCommand loginCommand)
        {
            var handler =
                new SaveChangesCommandDecorator<LoginCommand, Result<TokenDTO>>(
                    new ProfilerCommandDecorator<LoginCommand, Result<TokenDTO>>(
                        new LoggerCommandDecorator<LoginCommand, Result<TokenDTO>>(
                             new ValidationCommandDecorator<LoginCommand, Result<TokenDTO>>(
                                 new LoginCommandHandler(dbContext, tokenConfig)), logger
                )),null,dbContext);
            var result = handler.Handle(loginCommand);
            if (result.Failure)
                throw new Exception($"{nameof(loginCommand)} failed. Message: {result.Error}");
            return result.Value;
        }

        [HttpPost("refresh")]
        public TokenDTO RefreshToken([FromBody] TokenDTO tokenDTO)
        {
            var handler =
                new SaveChangesCommandDecorator<TokenDTO, Result<TokenDTO>>(
                    new ProfilerCommandDecorator<TokenDTO, Result<TokenDTO>>(
                        new LoggerCommandDecorator<TokenDTO, Result<TokenDTO>>(
                             new ValidationCommandDecorator<TokenDTO, Result<TokenDTO>>(
                                 new RefreshTokenCommandHandler(dbContext, tokenConfig)), logger
                )), null, dbContext);
            var result = handler.Handle(tokenDTO);
            if (result.Failure)
                throw new Exception($"{nameof(tokenDTO)} failed. Message: {result.Error}");
            return result.Value;
        }
    }
}