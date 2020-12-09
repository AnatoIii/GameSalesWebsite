using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Results
{
    /// <summary>
    /// Default implementation of <see cref="IResult"/>
    /// </summary>
    public class Result : IResult
    {
        /// <summary>
        /// <see cref="IResult.Success"/>
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// <see cref="IResult.Error"/>
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// <see cref="IResult.Failure"/>
        /// </summary>
        public bool Failure => !Success;

        protected Result(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        /// <summary>
        /// Returns new instatnce that failed with error message 
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns><see cref="Result"/></returns>
        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        /// <summary>
        /// Returns new instatnce that failed with error message 
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns><see cref="Result{T}"/></returns>
        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default, false, message);
        }

        /// <summary>
        /// Returns new instatnce with OK status
        /// </summary>
        /// <returns><see cref="Result"/></returns>
        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        /// <summary>
        /// Returns new instatnce with OK status
        /// </summary>
        /// <param name="value"><see cref="Result{T}.Value"/></param>
        /// <returns><see cref="Result{T}"/></returns>
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        /// <summary>
        /// Returns new instatnce that combines result of <paramref name="results"/>
        /// </summary>
        /// <param name="results">List of result</param>
        /// <returns><see cref="Result"/></returns>
        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.Failure)
                    return result;
            }

            return Ok();
        }
    }

    /// <summary>
    /// Generic version of <see cref="Result"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result
    {
        private T _value;

        /// <summary>
        /// Generic result value
        /// </summary>
        public T Value
        {
            get
            {
                if (Success)
                    return _value;

                return default;
            }
            [param: AllowNull]
            private set { _value = value; }
        }

        protected internal Result([AllowNull] T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }
    }
}
