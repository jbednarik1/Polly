﻿using System;
using System.Collections.Generic;
using Polly.Utilities;

namespace Polly.Retry
{
    partial class RetryPolicyStateWithSleep<TResult> : IRetryPolicyState<TResult>
    {
        readonly Context _context;
        readonly Action<DelegateResult<TResult>, TimeSpan, int, Context> _onRetry;
        readonly IEnumerator<TimeSpan> _sleepDurationsEnumerator;
        int _errorCount;

        public RetryPolicyStateWithSleep(IEnumerable<TimeSpan> sleepDurations, Action<DelegateResult<TResult>, TimeSpan, int, Context> onRetry, Context context)
        {
            _onRetry = onRetry;
            _context = context;
            _sleepDurationsEnumerator = sleepDurations.GetEnumerator();
        }

        public RetryPolicyStateWithSleep(IEnumerable<TimeSpan> sleepDurations, Action<DelegateResult<TResult>, TimeSpan, Context> onRetry, Context context)
            :
            this(sleepDurations, (delegateResult, span, i, c) => onRetry(delegateResult, span, c), context)
        {
        }

        public RetryPolicyStateWithSleep(IEnumerable<TimeSpan> sleepDurations, Action<DelegateResult<TResult>, TimeSpan> onRetry)
            :
            this(sleepDurations, (delegateResult, span, context) => onRetry(delegateResult, span), Context.Empty)
        {
        }

        public bool CanRetry(DelegateResult<TResult> delegateResult)
        {
            if (!_sleepDurationsEnumerator.MoveNext())
                return false;

            _errorCount += 1;

            var currentTimeSpan = _sleepDurationsEnumerator.Current;
            _onRetry(delegateResult, currentTimeSpan, _errorCount, _context);

            SystemClock.Sleep(currentTimeSpan);

            return true;
        }
    }
}
