using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DataAccess;
using GameSalesApi.Features.AccountManagement.CommandHandlers;
using GameSalesApi.Features.AccountManagement.Commands;
using GameSalesApi.Features.AccountManagement.Queries;
using GameSalesApi.Features.AccountManagement.QueryHandlers;
using Infrastructure.CommandBase;
using Infrastructure.InfrastructureCommandDecorators;
using Infrastructure.InfrastructureQueryDecorators;
using Infrastructure.Result;
using Model;

namespace GameSalesApi.Features.AccountManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DbContext _rDBContext;
        private readonly ICommandDispatcher _rCommandDispatcher;
        private readonly ILogger<AccountController> _rLogger;

        public AccountController(DbContext dbContext,
            ICommandDispatcher commandDispatcher,
            ILogger<AccountController> logger)
        {
            _rDBContext = dbContext;
            _rCommandDispatcher = commandDispatcher;
            _rLogger = logger;
        }

        // [DN] just an example of query pipeline
        [HttpGet(), Route("getAll")]
        public IEnumerable<User> GetAll()
        {
            GetUser getUser = new GetUser();

            var handler = 
                new ProfilerQueryDecorator<GetUser, Result<IEnumerable<User>>>(
                    new LoggerQueryDecorator<GetUser, Result<IEnumerable<User>>>(
                        new ValidationQueryDecorator<GetUser, Result<IEnumerable<User>>>(
                            new GetAllUsersByCountQueryHandler(
                                _rDBContext.Set<User>())),
                        _rLogger));

            var res = handler.Handle(getUser);

            if (res.Failure)
                throw new Exception($"{nameof(getUser)} failed. Message: {res.Error}");

            return res.Value;
        }
        
        // [DN] just an example of query pipeline
        [HttpGet(), Route("getAll")]
        public IEnumerable<User> GetUsersByCount(int usersCount)
        {
            GetUser getUser = new GetUser() { UsersMaxCount = usersCount};

            var handler = 
                new ProfilerQueryDecorator<GetUser, Result<IEnumerable<User>>>(
                    new LoggerQueryDecorator<GetUser, Result<IEnumerable<User>>>(
                        new ValidationQueryDecorator<GetUser, Result<IEnumerable<User>>>(
                            new GetAllUsersByCountQueryHandler(
                                _rDBContext.Set<User>())),
                        _rLogger));

            var res = handler.Handle(getUser);

            if (res.Failure)
                throw new Exception($"{nameof(getUser)} failed. Message: {res.Error}");

            return res.Value;
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
