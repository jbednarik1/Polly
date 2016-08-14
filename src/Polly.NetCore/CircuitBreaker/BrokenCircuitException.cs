using System;

namespace Polly.CircuitBreaker
{
    /// <summary>
    ///     Exception thrown when a circuit is broken.
    /// </summary>
    public class BrokenCircuitException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BrokenCircuitException" /> class.
        /// </summary>
        public BrokenCircuitException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BrokenCircuitException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BrokenCircuitException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BrokenCircuitException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The inner exception.</param>
        public BrokenCircuitException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    ///     Exception thrown when a circuit is broken.
    /// </summary>
    /// <typeparam name="TResult">The TResult type being handled by the policy.</typeparam>
    public class BrokenCircuitException<TResult> : BrokenCircuitException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BrokenCircuitException{TResult}" /> class.
        /// </summary>
        /// <param name="result">The result which caused the circuit to break.</param>
        public BrokenCircuitException(TResult result)
        {
            Result = result;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BrokenCircuitException{TResult}" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="result">The result which caused the circuit to break.</param>
        public BrokenCircuitException(string message, TResult result)
            : base(message)
        {
            Result = result;
        }

        /// <summary>
        ///     The result value which was considered a handled fault, by the policy.
        /// </summary>
        public TResult Result { get; }
    }
}
