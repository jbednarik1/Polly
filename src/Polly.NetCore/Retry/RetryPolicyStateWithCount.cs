using System;

namespace Polly.Retry
{
    partial class RetryPolicyStateWithCount<TResult> : IRetryPolicyState<TResult>
    {
        readonly Context _context;
        readonly Action<DelegateResult<TResult>, int, Context> _onRetry;
        readonly int _retryCount;
        int _errorCount;

        public RetryPolicyStateWithCount(int retryCount, Action<DelegateResult<TResult>, int, Context> onRetry, Context context)
        {
            _retryCount = retryCount;
            _onRetry = onRetry;
            _context = context;
        }

        public RetryPolicyStateWithCount(int retryCount, Action<DelegateResult<TResult>, int> onRetry)
            :
            this(retryCount, (delegateResult, i, context) => onRetry(delegateResult, i), Context.Empty)
        {
        }

        public bool CanRetry(DelegateResult<TResult> delegateResult)
        {
            _errorCount += 1;

            var shouldRetry = _errorCount <= _retryCount;
            if (shouldRetry)
                _onRetry(delegateResult, _errorCount, _context);

            return shouldRetry;
        }
    }
}
