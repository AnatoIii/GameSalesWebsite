using Infrastructure.HandlerBase;

namespace Infrastructure.CommandBase
{
    /// <summary>
    /// Base interface for commands
    /// </summary>
    /// <typeparam name="TOut">TOut</typeparam>
    public interface ICommand<TOut>
    { }

    /// <summary>
    /// Base Command handler
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut</typeparam>
    public interface ICommandHandler<in TIn, out TOut>
        : IHandler<TIn, TOut>
        where TIn : ICommand<TOut>
    {
        /// <summary>
        /// Command perfomer
        /// </summary>
        /// <param name="command">TIn command</param>
        void Execute(TIn command);
    }

    /// <summary>
    /// Base interface for command dispatcher
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatcher handler
        /// </summary>
        /// <typeparam name="TIn">TIn</typeparam>
        /// <typeparam name="Tout">TOut</typeparam>
        /// <param name="command">TIn command</param>
        void Handle<TIn, Tout>(TIn command) where TIn : ICommand<Tout>;
    }
}
