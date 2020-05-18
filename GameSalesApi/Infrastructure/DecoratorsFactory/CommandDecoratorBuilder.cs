using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.CommandBase;
using Infrastructure.Exceptions;
using Infrastructure.HandlerBase;

namespace Infrastructure.DecoratorsFactory
{
    /// <summary>
    /// Command decorator chains builder
    /// </summary>
    /// <typeparam name="TIn"><see cref="ICommand{TOut}"/> to be handled</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="CommandHandlerDecoratorBase{TIn, TOut}.Handle(TIn)"</typeparam>
    public class CommandDecoratorBuilder<TIn, TOut> 
        : DecoratorBuilder<TIn, TOut>
        where TIn : ICommand<TOut>
    {
        /// <summary>
        /// Adds a new decorator to the decorators chain. This decorator will be initilized next.
        /// It must have a decorated object as the first parameter. Type can be of any subtypes of <see cref="CommandHandlerDecoratorBase{TIn, TOut}" />
        /// and cannot be asbtract.
        /// </summary>
        /// <typeparam name="Handler">Your decorator, inherited from handlerBase <see cref="CommandHandlerDecoratorBase{TIn, TOut}"/></typeparam>
        public CommandDecoratorBuilder<TIn, TOut> Add<Handler>() where Handler : CommandHandlerDecoratorBase<TIn, TOut>
        {
            Type type = typeof(Handler);

            if (type.IsAbstract)
                throw new ArgumentException("Handler can't be abstract");

            if (_pCurrentNode != null)
                _pDecorators.AddLast(_pCurrentNode);

            _pCurrentNode = new HandlerNode(type);

            return this;
        }

        /// <summary>
        /// Adds an argument to the handler constructor.
        /// </summary>
        /// <typeparam name="PType">The Type of the parameter</typeparam>
        /// <param name="param">Parameter value</param>
        public CommandDecoratorBuilder<TIn, TOut> AddParameter<PType>(object param)
        {
            if (_pCurrentNode == null)
                throw new NullDecoratorException("No current decorator to add parameter to");

            _pCurrentNode.AddArgument(typeof(PType), param);
            return this;
        }

        /// <summary>
        /// <see cref="DecoratorBuilder{TIn, TOut}.CreateObject(IHandler{TIn, TOut}, HandlerNode)"/>
        /// </summary>
        private protected override IHandler<TIn, TOut> CreateObject(IHandler<TIn, TOut> prevConstructed, HandlerNode node)
        {
            Type handlerType = node.GetHandlerType();

            LinkedList<ArgumentNode> arguments = node.GetArguments();
            if (prevConstructed != null)
            {
                arguments.AddFirst(new ArgumentNode()
                {
                    Type = typeof(CommandHandlerDecoratorBase<TIn, TOut>),
                    Value = prevConstructed
                });
            }
            Type[] argumentTypes = arguments.Select(a => a.Type).ToArray();
            object[] argumentValues = arguments.Select(a => a.Value).ToArray();

            object decorator = handlerType.GetConstructor(argumentTypes).Invoke(argumentValues);

            return (CommandHandlerDecoratorBase<TIn, TOut>)decorator;
        }
    }
}
