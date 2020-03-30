using Infrastructure.CommandBase;
using Infrastructure.HandlerBase;
using System;
using System.Security;

namespace Infrastructure.InfrastructureCommandDecorators
{
    /// <summary>
    /// Command decorator for validation
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="CommandHandlerDecoratorBase{TIn, TOut}.Handle(TIn)"</typeparam>
    public class ValidationCommandDecorator<TIn, TOut>
        : CommandHandlerDecoratorBase<TIn, TOut>
        where TIn : ICommand<TOut>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="decorated"><see cref="ICommandHandler{TIn, TOut}"/> inner decorator</param>
        public ValidationCommandDecorator(ICommandHandler<TIn, TOut> decorated)
            : base(decorated)
        { }

        /// <summary>
        /// Perform validation for inner decorators
        /// </summary>
        /// <param name="command">TIn command</param>
        public override void Execute(TIn command)
        {
            if (!_CheckPermission(command))
            {
                throw new SecurityException();
            }
            _rDecorated.Execute(command);
        }

        /// <summary>
        /// Perform validation for inner decorators
        /// </summary>
        /// <param name="input">TIn command</param>
        /// <returns>TOut</returns>
        public override TOut Handle(TIn input)
        {
            //if (!_CheckPermission(input))
            //{
            //    throw new SecurityException();
            //}
            return _rDecorated.Handle(input);
        }

        private bool _CheckPermission(TIn input)
        {
            throw new NotImplementedException();
        }
    }
}
