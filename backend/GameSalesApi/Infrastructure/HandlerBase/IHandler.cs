namespace Infrastructure.HandlerBase
{
    /// <summary>
    /// Base interface for Handling
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut</typeparam>
    public interface IHandler<in TIn, out TOut>
    {
        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="input">TIn</param>
        /// <returns>TOut</returns>
        TOut Handle(TIn input);
    }
}
