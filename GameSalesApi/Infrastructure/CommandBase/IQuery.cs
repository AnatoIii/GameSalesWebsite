using Infrastructure.HandlerBase;

namespace Infrastructure.CommandBase
{
    /// <summary>
    /// Base interface for query
    /// </summary>
    /// <typeparam name="TOut">TOut</typeparam>
    public interface IQuery<TOut>
    { }

    /// <summary>
    /// Base Query handler
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut</typeparam>
    public interface IQueryHandler<in TIn, out TOut>
        : IHandler<TIn, TOut>
        where TIn : IQuery<TOut>
    { }
}
