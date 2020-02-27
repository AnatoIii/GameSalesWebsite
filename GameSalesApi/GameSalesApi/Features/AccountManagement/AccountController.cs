using Infrastructure.CommandBase;
using Infrastructure.InfrastructureCommandDecorators;
using Infrastructure.InfrastructureQueryDecorators;
using Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;

namespace GameSalesApi.Features.AccountManagement
{
    [ApiController]
    [Route("[controller]")]
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

        // [DN] just an example of command pipeline
        [HttpPost(), DisableRequestSizeLimit, Route("updateEmail")]
        public void UpdateEmail(UpdateUserEmail updateUserEmail)
        {
            var handler =
                new SaveChangesCommandDecorator<UpdateUserEmail, Result>(
                    new ProfilerCommandDecorator<UpdateUserEmail, Result>(
                        new LoggerCommandDecorator<UpdateUserEmail, Result>(
                            new ValidationCommandDecorator<UpdateUserEmail, Result>(
                                new UpdateEmailCommandHandler(
                                    _rDBContext.Set<User>())),
                            _rLogger)),
                    _rCommandDispatcher, _rDBContext);

            handler.Execute(updateUserEmail);
        }
    }
}
