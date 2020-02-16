using Infrastructure.CommandBase;
using Infrastructure.HandlerBase;
using StackExchange.Profiling;

namespace Infrastructure.InfrastructureQueryDecorators
{
    /// <summary>
    /// Query decorator for profiling
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="QueryHandlerDecoratorBase{TIn, TOut, TExecuteOut}.Handle(TIn)"</typeparam>
    /// <typeparam name="TExecuteOut"><see cref="QueryHandlerDecoratorBase{TIn, TOut, TExecuteOut}.Execute(TIn)"/></typeparam>
    public class ProfilerQueryDecorator<TIn, TOut, TExecuteOut>
        : QueryHandlerDecoratorBase<TIn, TOut, TExecuteOut>
        where TIn : IQuery<TOut>
        where TExecuteOut : class
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="decorated"><see cref="IQueryHandler{TIn, TOut, TExecute}"/> inner decorator</param>
        public ProfilerQueryDecorator(IQueryHandler<TIn, TOut, TExecuteOut> decorated) 
            : base(decorated)
        { }

        /// <summary>
        /// Perform profiling for inner decorators
        /// </summary>
        /// <param name="query">TIn query</param>
        public override TExecuteOut Execute(TIn query)
        {
            using (MiniProfiler.Current.Step(_rDecorated.GetType().ToString()))
            {
                return _rDecorated.Execute(query);
            }
        }

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
