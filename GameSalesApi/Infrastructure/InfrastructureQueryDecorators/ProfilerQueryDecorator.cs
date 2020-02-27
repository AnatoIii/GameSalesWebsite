using Infrastructure.CommandBase;
using Infrastructure.HandlerBase;
using StackExchange.Profiling;

namespace Infrastructure.InfrastructureQueryDecorators
{
    /// <summary>
    /// Query decorator for profiling
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="QueryHandlerDecoratorBase{TIn, TOut}.Handle(TIn)"</typeparam>
    public class ProfilerQueryDecorator<TIn, TOut>
        : QueryHandlerDecoratorBase<TIn, TOut>
        where TIn : IQuery<TOut>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="decorated"><see cref="IQueryHandler{TIn, TOut}"/> inner decorator</param>
        public ProfilerQueryDecorator(IQueryHandler<TIn, TOut> decorated) 
            : base(decorated)
        { }

        /// <summary>
        /// Perform profiling for inner decorators
        /// </summary>
        /// <param name="input">TIn query</param>
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
