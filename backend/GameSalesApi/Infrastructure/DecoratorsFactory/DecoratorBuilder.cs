using System.Collections.Generic;
using Infrastructure.HandlerBase;

namespace Infrastructure.DecoratorsFactory
{
    /// <summary>
    /// Default class for decorator chains builder
    /// </summary>
    /// <typeparam name="TIn"><see cref="ICommand{TOut}"/> or <see cref="IQuery{TOut}"/> to be handled</typeparam>
    /// <typeparam name="TOut">TOut, can be used like output type in <see cref="IHandler{TIn, TOut}.Handle(TIn)"</typeparam>
    public abstract class DecoratorBuilder<TIn, TOut>
    {
        private protected LinkedList<HandlerNode> _pDecorators;
        private protected HandlerNode _pCurrentNode;

        /// <summary>
        /// Default ctor
        /// </summary>
        public DecoratorBuilder()
        {
            _pDecorators = new LinkedList<HandlerNode>();
        }

        /// <summary>
        /// Builds the chain of decorators and returns resulting handler
        /// </summary>
        public IHandler<TIn, TOut> Build()
        {
            if (_pCurrentNode == null)
                return null;

            _pDecorators.AddLast(_pCurrentNode);

            var node = _pDecorators.First;
            IHandler<TIn, TOut> handler = null;

            while (node != null)
            {
                handler = CreateObject(handler, node.Value);
                node = node.Next;
            }

            return handler;
        }

        /// <summary>
        /// A Helper function for constructing a decorator in the decorators chain
        /// </summary>
        /// <param name="node">Next <see cref="HandlerNode"/> to be processed</param>
        /// <param name="prevConstructed">A Decorator, constructed on the previous step</param>
        private protected abstract IHandler<TIn, TOut> CreateObject(IHandler<TIn, TOut> prevConstructed, HandlerNode node);
    }
}
