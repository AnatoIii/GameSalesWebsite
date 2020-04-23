using Infrastructure.CommandBase;
using Infrastructure.Result;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.InfrastructureCommandDecorators
{
    public static class CommandDecoratorBuilderExtensions
    {
        public static CommandDecoratorBuilder<TIn, TOut> AddBaseDecorators<TIn, TOut, ClientType>
            (this CommandDecoratorBuilder<TIn, TOut> builder, ILogger<ClientType> logger, DbContext dbContext) where TIn : ICommand<TOut>
        {
            return builder
                .Add<ValidationCommandDecorator<TIn, TOut>>()
                .Add<LoggerCommandDecorator<TIn, TOut>>()
                    .AddParameter<ILogger<ClientType>>(logger)
                .Add<ProfilerCommandDecorator<TIn, TOut>>()
                .Add<SaveChangesCommandDecorator<TIn, TOut>>()
                    .AddParameter<ICommandDispatcher>(null)
                    .AddParameter<DbContext>(dbContext);
        }
    }
}
