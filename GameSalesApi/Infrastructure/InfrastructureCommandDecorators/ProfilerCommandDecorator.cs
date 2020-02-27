using Infrastructure.CommandBase;
using Infrastructure.HandlerBase;
using StackExchange.Profiling;

namespace Infrastructure.InfrastructureCommandDecorators
{
    /// <summary>
    /// Command decorator for profiling
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="CommandHandlerDecoratorBase{TIn, TOut}.Handle(TIn)"</typeparam>
    public class ProfilerCommandDecorator<TIn, TOut>
        : CommandHandlerDecoratorBase<TIn, TOut>
        where TIn : ICommand<TOut>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="decorated"><see cref="ICommandHandler{TIn, TOut}"/> inner decorator</param>
        public ProfilerCommandDecorator(ICommandHandler<TIn, TOut> decorated) 
            : base(decorated)
        { }

        /// <summary>
        /// Perform profiling for inner decorators
        /// </summary>
        /// <param name="command">TIn command</param>
        public override void Execute(TIn command)
        {
            using (MiniProfiler.Current.Step(_rDecorated.GetType().ToString()))
            {
                _rDecorated.Execute(command);
            }
        }

        /// <summary>
        /// Perform profiling for inner decorators
        /// </summary>
        /// <param name="input">TIn command</param>
        /// <returns>TOut</returns>
        public override TOut Handle(TIn input)
        {
            using (MiniProfiler.Current.Step(_rDecorated.GetType().ToString()))
            {
                return _rDecorated.Handle(input);
            }
        }
    }
}
