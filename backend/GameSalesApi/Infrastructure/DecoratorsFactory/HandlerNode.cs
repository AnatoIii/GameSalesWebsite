using System;
using System.Collections.Generic;

namespace Infrastructure.DecoratorsFactory
{
    /// <summary>
    /// Represent class for store decorators in <see cref="DecoratorBuilder{TIn, TOut}"/> and it inheritors
    /// </summary>
    internal class HandlerNode
    {
        private readonly Type _handlerType;
        private LinkedList<ArgumentNode> _arguments;

        /// <summary>
        /// Gets handled type
        /// </summary>
        internal Type GetHandlerType() => _handlerType;

        /// <summary>
        /// Gets argument for current <see cref="HandlerNode"/>
        /// </summary>
        internal LinkedList<ArgumentNode> GetArguments() => _arguments;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="handlerType">Target type</param>
        internal HandlerNode(Type handlerType)
        {
            _arguments = new LinkedList<ArgumentNode>();
            _handlerType = handlerType;
        }

        /// <summary>
        /// Add <paramref name="argumentType"/> and <paramref name="value"/> to list of arguments
        /// </summary>
        /// <param name="argumentType">Target argument type</param>
        /// <param name="value">Target value</param>
        internal void AddArgument(Type argumentType, object value)
            => _arguments.AddLast(new ArgumentNode() { Type = argumentType, Value = value });
    }
}
