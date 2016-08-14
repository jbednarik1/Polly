using System;

namespace Polly
{
    /// <summary>
    ///     The captured outcome of executing an individual Func&lt;TResult&gt;
    /// </summary>
    public class DelegateResult<TResult>
    {
        internal DelegateResult(TResult result)
        {
            Result = result;
        }

        internal DelegateResult(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        ///     The result of executing the delegate. Will be default(TResult) if an exception was thrown.
        /// </summary>
        public TResult Result { get; }

        /// <summary>
        ///     Any exception thrown while executing the delegate. Will be null if policy executed without exception.
        /// </summary>
        public Exception Exception { get; }
    }
}
