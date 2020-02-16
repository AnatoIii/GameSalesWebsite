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
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="QueryHandlerDecoratorBase{TIn, TOut, TExecuteOut}.Handle(TIn)"</typeparam>
    /// <typeparam name="TExecuteOut"><see cref="QueryHandlerDecoratorBase{TIn, TOut, TExecuteOut}.Execute(TIn)"/></typeparam>
    public class ValidationQueryDecorator<TIn, TOut, TExecuteOut>
        : QueryHandlerDecoratorBase<TIn, TOut, TExecuteOut>
        where TIn : IQuery<TOut>
        where TExecuteOut : class
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="decorated"><see cref="IQueryHandler{TIn, TOut, TExecute}"/> inner decorator</param>
        public ValidationQueryDecorator(IQueryHandler<TIn, TOut, TExecuteOut> decorated)
            : base(decorated)
        { }

        /// <summary>
        /// Perform validation for inner decorators
        /// </summary>
        /// <param name="query">TIn query</param>
        public override TExecuteOut Execute(TIn query)
        {
            if (!_CheckPermission(query))
            {
                throw new SecurityException();
            }
            return _rDecorated.Execute(query);
        }

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
