using Infrastructure.CommandBase;
using Infrastructure.HandlerBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Infrastructure.Exceptions;

namespace Infrastructure.InfrastructureCommandDecorators
{
    /// <summary>
    /// Command decorator chains builder
    /// </summary>
    /// <typeparam name="TIn">ICommand<TOut> to be handled</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="CommandHandlerDecoratorBase{TIn, TOut}.Handle(TIn)"</typeparam>
    public class CommandDecoratorBuilder<TIn, TOut> where TIn : ICommand<TOut>
    {
        private LinkedList<CommandHandlerNode> _decorators;
        private CommandHandlerNode _currentNode;

        public CommandDecoratorBuilder()
        {
            _decorators = new LinkedList<CommandHandlerNode>();
        }

        /// <summary>
        /// Builds the chain of decorators and returns resulting handler
        /// </summary>
        /// <param name="command">TIn command</param>
        public CommandHandlerDecoratorBase<TIn, TOut> Build()
        {
            if(_currentNode == null)
            {
                return null;
            }
            _decorators.AddLast(_currentNode);

            var node = _decorators.First;
            CommandHandlerDecoratorBase<TIn, TOut> handler = null;

            while (node != null)
            {
                handler = CreateObject(handler, node.Value);
                node = node.Next;
            }

            return handler;
        }

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
            {
                throw new ArgumentException("Handler can't be abstract");
            }

            if(_currentNode != null)
            {
                _decorators.AddLast(_currentNode);
            }
            _currentNode = new CommandHandlerNode(type);
            return this;
        }

        /// <summary>
        /// Adds an argument to the handler constructor.
        /// </summary>
        /// <typeparam name="PType">The Type of the parameter</typeparam>
        /// <param name="param">Parameter value</param>
        public CommandDecoratorBuilder<TIn, TOut> AddParameter<PType>(object param)
        {
            if(_currentNode == null)
            {
                throw new NullDecoratorException("No current decorator to add parameter to");
            }
            _currentNode.AddArgument(typeof(PType), param);
            return this;
        }

        /// <summary>
        /// A Helper function for constructing a decorator in the decorators chain
        /// </summary>
        /// <param name="node">Next CommandHandlerNode to be processed</param>
        /// <param name="prevConstructed">A Decorator, constructed on the previous step</param>
        private static CommandHandlerDecoratorBase<TIn, TOut> CreateObject(CommandHandlerDecoratorBase<TIn, TOut> prevConstructed, CommandHandlerNode node)
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

            return (CommandHandlerDecoratorBase<TIn, TOut>) decorator;
        }

        /// <summary>
        /// Internal class for representing Arguments Type And Value
        /// </summary>
        private class ArgumentNode
        {
            public Type Type { get; set; }
            public object Value { get; set; }
        }

        /// <summary>
        /// Internal class for representing a handler Type and All of it's constructor parameters
        /// </summary>
        private class CommandHandlerNode
        {
            private readonly Type _handlerType;
            private LinkedList<ArgumentNode> _arguments;

            public Type GetHandlerType() => _handlerType;
            public LinkedList<ArgumentNode> GetArguments() => _arguments;

            public CommandHandlerNode(Type handlerType)
            {
                _arguments = new LinkedList<ArgumentNode>();
                _handlerType = handlerType;
            }

            public void AddArgument(Type argumentType, object value)
            {
                _arguments.AddLast(new ArgumentNode() {Type = argumentType, Value = value});
            }
        }
    }
}
