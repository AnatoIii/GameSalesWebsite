using Infrastructure.CommandBase;
using Infrastructure.HandlerBase;
using Microsoft.Extensions.Logging;
using System;

namespace Infrastructure.InfrastructureQueryDecorators
{
    /// <summary>
    /// Query decorator for logging
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="QueryHandlerDecoratorBase{TIn, TOut, TExecuteOut}.Handle(TIn)"</typeparam>
    /// <typeparam name="TExecuteOut"><see cref="QueryHandlerDecoratorBase{TIn, TOut, TExecuteOut}.Execute(TIn)"/></typeparam>
    public class LoggerQueryDecorator<TIn, TOut, TExecuteOut>
        : QueryHandlerDecoratorBase<TIn, TOut, TExecuteOut>
        where TIn : IQuery<TOut>
        where TExecuteOut : class
    {
        private readonly ILogger _rLogger;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="decorated"><see cref="IQueryHandler{TIn, TOut, TExecute}"/> inner decorator</param>
        /// <param name="logger"><see cref="ILogger"/> logger</param>
        public LoggerQueryDecorator(IQueryHandler<TIn, TOut, TExecuteOut> decorated,
            ILogger logger)
            : base(decorated)
        {
            if (decorated is null)
                throw new ArgumentNullException(nameof(decorated));

            _rLogger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Perform logging for inner decorators
        /// </summary>
        /// <param name="query">TIn query</param>
        public override TExecuteOut Execute(TIn query)
        {
            TExecuteOut output = default;

            try
            {
                _rDecorated.Execute(query);
                _rLogger.LogInformation(
                    $"{_rDecorated.GetType()}: {query} => successfully");
            }
            catch (Exception e)
            {
                _rLogger.LogError($"Error ocurred: {query}, ex: {e.Message}");
            }

            return output;
        }

        /// <summary>
        /// Perform logging for inner decorators
        /// </summary>
        /// <param name="input">TIn query</param>
        /// <returns>TOut</returns>
        public override TOut Handle(TIn input)
        {
            TOut output = default;

            try
            {
                output = _rDecorated.Handle(input);
                _rLogger.LogInformation(
                    $"{_rDecorated.GetType()}: {input} => {output}");
            }
            catch (Exception e)
            {
                _rLogger.LogError($"Error ocurred: {input}, ex: {e.Message}");
            }

            return output;
        }
    }
}
