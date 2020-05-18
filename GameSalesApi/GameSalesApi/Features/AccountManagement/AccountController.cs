using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataAccess;
using GameSalesApi.Features.AccountManagement.CommandHandlers;
using GameSalesApi.Features.AccountManagement.Commands;
using GameSalesApi.Features.AccountManagement.Queries;
using GameSalesApi.Features.AccountManagement.QueryHandlers;
using Infrastructure.DecoratorsFactory;
using Infrastructure.Result;
using Model;

namespace GameSalesApi.Features.AccountManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly GameSalesContext _rDBContext;
        private readonly ILogger<AccountController> _rLogger;

        public AccountController(GameSalesContext dbContext,
            ILogger<AccountController> logger)
        {
            _rDBContext = dbContext;
            _rLogger = logger;
        }

        [HttpGet(), Route("{id}")]
        public IActionResult GetUser([FromQuery] GetUserQuery getUserQuery)
        {
            var handler = new QueryDecoratorBuilder<GetUserQuery, Result<User>>()
                .Add<GetUserQueryHandler>()
                    .AddParameter<GameSalesContext>(_rDBContext)
                .AddBaseDecorators(_rLogger)
                .Build();

            var result = handler.Handle(getUserQuery);

            if (result.Failure)
                return BadRequest($"{nameof(getUserQuery)} failed. Message: {result.Error}");

            return Ok(result.Value);
        }

        [HttpGet(), Route("getAll")]
        public IActionResult GetAll()
        {
            var handler = new QueryDecoratorBuilder<GetAllUsersQuery, Result<IEnumerable<User>>>()
                .Add<GetAllUsersQueryHandler>()
                    .AddParameter<GameSalesContext>(_rDBContext)
                .AddBaseDecorators(_rLogger)
                .Build();

            var getAllUsersQuery = new GetAllUsersQuery();

            var result = handler.Handle(getAllUsersQuery);

            if (result.Failure)
                return BadRequest($"{nameof(getAllUsersQuery)} failed. Message: {result.Error}");

            return Ok(result.Value);
        }

        [HttpPost(), Route("update")]
        public IActionResult UpdateUser(UpdateUserCommand updateUserCommand)
        {
            var handler = new CommandDecoratorBuilder<UpdateUserCommand, Result>()
                .Add<UpdateUserCommandHandler>()
                    .AddParameter<GameSalesContext>(_rDBContext)
                .AddBaseDecorators(_rLogger, _rDBContext)
                .Build();

            var result = handler.Handle(updateUserCommand);

            if (result.Failure)
                return BadRequest($"{nameof(updateUserCommand)} failed. Message: {result.Error}");

            return Ok();
        }

        [HttpPost(), Route("remove")]
        public IActionResult RemoveUser(RemoveUserCommand removeUserCommand)
        {
            var handler = new CommandDecoratorBuilder<RemoveUserCommand, Result>()
               .Add<RemoveUserCommandHandler>()
                   .AddParameter<GameSalesContext>(_rDBContext)
               .AddBaseDecorators(_rLogger, _rDBContext)
               .Build();

            var result = handler.Handle(removeUserCommand);

            if (result.Failure)
                return BadRequest($"{nameof(removeUserCommand)} failed. Message: {result.Error}");

            return Ok();
        }
    }
}
