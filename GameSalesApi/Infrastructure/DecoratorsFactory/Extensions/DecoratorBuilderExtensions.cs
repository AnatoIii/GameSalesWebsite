using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Infrastructure.CommandBase;
using Infrastructure.InfrastructureCommandDecorators;
using Infrastructure.InfrastructureQueryDecorators;

namespace Infrastructure.DecoratorsFactory
{
    /// <summary>
    /// Extensions for <see cref="DecoratorBuilder{TIn, TOut}"/> and it inheritors 
    /// <see cref="CommandDecoratorBuilder{TIn, TOut}"/> and <see cref="QueryDecoratorBuilder{TIn, TOut}"/>
    /// </summary>
    public static class DecoratorBuilderExtensions
    {
        /// <summary>
        /// Gets default command decorators for any purpose. The order of decorators (from the first to the last):
        /// <see cref="SaveChangesCommandDecorator{TIn, TOut}"/>, 
        /// <see cref="ProfilerCommandDecorator{TIn, TOut}"/>,
        /// <see cref="LoggerCommandDecorator{TIn, TOut}"/> and
        /// <see cref="ValidationCommandDecorator{TIn, TOut}"/>
        /// </summary>
        /// <typeparam name="TIn"><see cref="ICommand{TOut}"/> to be handled</typeparam>
        /// <typeparam name="TOut">TOut for <see cref="ICommand{TOut}"/></typeparam>
        /// <typeparam name="ClientType">Client type for <paramref name="logger"/></typeparam>
        /// <param name="builder">This</param>
        /// <param name="logger">Logger <see cref="ILogger"/> for <see cref="LoggerCommandDecorator{TIn, TOut}"/></param>
        /// <param name="dbContext"><see cref="DbContext"/> for <see cref="SaveChangesCommandDecorator{TIn, TOut}"/></param>
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

        /// <summary>
        /// Gets default query decorators for any purpose. The order of decorators (from the first to the last):
        /// <see cref="ProfilerQueryDecorator{TIn, TOut}"/>,
        /// <see cref="LoggerQueryDecorator{TIn, TOut}"/> and
        /// <see cref="ValidationQueryDecorator{TIn, TOut}"/>
        /// </summary>
        /// <typeparam name="TIn"><see cref="IQuery{TOut}"/> to be handled</typeparam>
        /// <typeparam name="TOut">TOut for <see cref="IQuery{TOut}"/></typeparam>
        /// <typeparam name="ClientType">Client type for <paramref name="logger"/></typeparam>
        /// <param name="builder">This</param>
        /// <param name="logger">Logger <see cref="ILogger"/> for <see cref="LoggerQueryDecorator{TIn, TOut}"/></param>
        public static QueryDecoratorBuilder<TIn, TOut> AddBaseDecorators<TIn, TOut, ClientType>
            (this QueryDecoratorBuilder<TIn, TOut> builder, ILogger<ClientType> logger) where TIn : IQuery<TOut>
        {
            return builder
                .Add<ValidationQueryDecorator<TIn, TOut>>()
                .Add<LoggerQueryDecorator<TIn, TOut>>()
                    .AddParameter<ILogger<ClientType>>(logger)
                .Add<ProfilerQueryDecorator<TIn, TOut>>();
        }
    }
}
