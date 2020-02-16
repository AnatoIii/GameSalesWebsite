using Infrastructure.CommandBase;

namespace Infrastructure.HandlerBase
{
    /// <summary>
    /// Command handler decorator base for all commands
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="QueryHandlerDecoratorBase{TIn, TOut, TExecuteOut}.Handle(TIn)"</typeparam>
    public abstract class CommandHandlerDecoratorBase<TIn, TOut>
        : ICommandHandler<TIn, TOut>
        where TIn : ICommand<TOut>
    {
        protected readonly ICommandHandler<TIn, TOut> _rDecorated;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="decorated"><see cref="ICommandHandler{TIn, TOut}"/> inner decorator</param>
        protected CommandHandlerDecoratorBase(ICommandHandler<TIn, TOut> decorated)
        {
            _rDecorated = decorated;
        }

        /// <summary>
        /// Command perfomer
        /// </summary>
        /// <param name="command">TIn command</param>
        public abstract void Execute(TIn command);

        /// <summary>
        /// Command perfomer with generic output
        /// </summary>
        /// <param name="input">TIn command</param>
        /// <returns>TOut</returns>
        public abstract TOut Handle(TIn input);
    }

    /// <summary>
    /// Query handler decorator base for all commands
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="QueryHandlerDecoratorBase{TIn, TOut, TExecuteOut}.Handle(TIn)"</typeparam>
    /// <typeparam name="TExecuteOut">TExecuteOut, used in <see cref="QueryHandlerDecoratorBase{TIn, TOut, TExecuteOut}.Execute(TIn)"/></typeparam>
    public abstract class QueryHandlerDecoratorBase<TIn, TOut, TExecuteOut>
       : IQueryHandler<TIn, TOut, TExecuteOut>
       where TIn : IQuery<TOut>
       where TExecuteOut : class
    {
        protected readonly IQueryHandler<TIn, TOut, TExecuteOut> _rDecorated;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="decorated"><see cref="IQueryHandler{TIn, TOut, TExecute}"/> inner decorator</param>
        protected QueryHandlerDecoratorBase(IQueryHandler<TIn, TOut, TExecuteOut> decorated)
        {
            _rDecorated = decorated;
        }

        /// <summary>
        /// Query perfomer
        /// </summary>
        /// <param name="command">TIn command</param>
        public abstract TExecuteOut Execute(TIn query);

        /// <summary>
        /// Query perfomer with generic output
        /// </summary>
        /// <param name="input">TIn command</param>
        /// <returns>TOut</returns>
        public abstract TOut Handle(TIn input);
    }
}
