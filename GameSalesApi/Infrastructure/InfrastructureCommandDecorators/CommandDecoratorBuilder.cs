using Infrastructure.CommandBase;
using Infrastructure.HandlerBase;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;

namespace Infrastructure.InfrastructureCommandDecorators
{
    public class CommandDecoratorBuilder<TIn, TOut> where TIn : ICommand<TOut>
    {
        private LinkedList<CommandHandlerNode> _decorators;
        private CommandHandlerNode _currentNode;
        public CommandDecoratorBuilder()
        {
            _decorators = new LinkedList<CommandHandlerNode>();
        }
        public CommandHandlerDecoratorBase<TIn, TOut> Build()
        {
            if(_currentNode != null)
            {
                _decorators.AddLast(_currentNode);
            }

            var node = _decorators.First;
            CommandHandlerDecoratorBase<TIn, TOut> handler = null;

            while (node != null)
            {
                handler = CreateObject(handler, node.Value);
                node = node.Next;
            }

            return handler;
        }

        public CommandDecoratorBuilder<TIn, TOut> Add<Handler>() where Handler : CommandHandlerDecoratorBase<TIn, TOut>
        {
            if(_currentNode != null)
            {
                _decorators.AddLast(_currentNode);
            }
            _currentNode = new CommandHandlerNode(typeof(Handler));
            return this;
        }

        public CommandDecoratorBuilder<TIn, TOut> AddParameter<PType>(object param)
        {
            if(_currentNode != null)
            {
                _currentNode.AddArgument(typeof(PType), param);
            }
            return this;
        }
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

        private class ArgumentNode
        {
            public Type Type { get; set; }
            public object Value { get; set; }
        }

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
