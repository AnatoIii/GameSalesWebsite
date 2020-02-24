using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Result
{
    /// <summary>
    /// Interface for Result of command/query
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Result success flag
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Result error description
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Result failure flag
        /// </summary>
        public bool Failure { get; }

    }
}
