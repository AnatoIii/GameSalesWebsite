using Infrastructure.CommandBase;
using Infrastructure.HandlerBase;
using System;
using System.Security;

namespace Infrastructure.InfrastructureQueryDecorators
{
    /// <summary>
    /// Query decorator for validation
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="QueryHandlerDecoratorBase{TIn, TOut}.Handle(TIn)"</typeparam>
    public class ValidationQueryDecorator<TIn, TOut>
        : QueryHandlerDecoratorBase<TIn, TOut>
        where TIn : IQuery<TOut>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="decorated"><see cref="IQueryHandler{TIn, TOut}"/> inner decorator</param>
        public ValidationQueryDecorator(IQueryHandler<TIn, TOut> decorated)
            : base(decorated)
        { }

        /// <summary>
        /// Perform validation for inner decorators
        /// </summary>
        /// <param name="input">TIn query</param>
        /// <returns>TOut</returns>
        public override TOut Handle(TIn input)
        {
            if (!_CheckPermission(input))
            {
                throw new SecurityException();
            }
            return _rDecorated.Handle(input);
        }

        private bool _CheckPermission(TIn input)
        {
            throw new NotImplementedException();
        }
    }
}
