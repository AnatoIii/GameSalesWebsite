using Infrastructure.CommandBase;
using Infrastructure.HandlerBase;
using Microsoft.Extensions.Logging;
using System;

namespace Infrastructure.InfrastructureCommandDecorators
{
    /// <summary>
    /// Command decorator for logging
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="CommandHandlerDecoratorBase{TIn, TOut}.Handle(TIn)"</typeparam>
    public class LoggerCommandDecorator<TIn, TOut>
        : CommandHandlerDecoratorBase<TIn, TOut>
        where TIn : ICommand<TOut>
    {
        private readonly ILogger _rLogger;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="decorated"><see cref="ICommandHandler{TIn, TOut}"/> inner decorator</param>
        /// <param name="logger"><see cref="ILogger"/> logger</param>
        public LoggerCommandDecorator(ICommandHandler<TIn, TOut> decorated,
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
        /// <param name="command">TIn command</param>
        public override void Execute(TIn command)
        {
            try
            {
                _rDecorated.Execute(command);
                _rLogger.LogInformation(
                    $"{_rDecorated.GetType()}: {command} => successfully");
            }
            catch (Exception e)
            {
                _rLogger.LogError($"Error ocurred: {command}, ex: {e.Message}");
            }
        }

        /// <summary>
        /// Perform logging for inner decorators
        /// </summary>
        /// <param name="input">TIn command</param>
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
