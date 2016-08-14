﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Polly.Retry
{
    partial class RetryPolicyState<TResult> : IRetryPolicyState<TResult>
    {
        readonly Func<DelegateResult<TResult>, Context, Task> _onRetryAsync;

        public RetryPolicyState(Func<DelegateResult<TResult>, Context, Task> onRetryAsync, Context context)
        {
            _onRetryAsync = onRetryAsync;
            _context = context;
        }

        public RetryPolicyState(Func<DelegateResult<TResult>, Task> onRetryAsync)
            :
            this((delegateResult, context) => onRetryAsync(delegateResult), Context.Empty)
        {
        }

        public async Task<bool> CanRetryAsync(DelegateResult<TResult> delegateResult, CancellationToken ct, bool continueOnCapturedContext)
        {
            await _onRetryAsync(delegateResult, _context).ConfigureAwait(continueOnCapturedContext);
            return true;
        }
    }
}
