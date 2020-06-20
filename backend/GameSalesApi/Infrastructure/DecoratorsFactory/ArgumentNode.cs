using System;

namespace Infrastructure.DecoratorsFactory
{
    /// <summary>
    /// Internal class for representing Arguments Type And Value
    /// </summary>
    internal class ArgumentNode
    {
        /// <summary>
        /// Argument type
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Argument value
        /// </summary>
        public object Value { get; set; }
    }
}
