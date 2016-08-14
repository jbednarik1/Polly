﻿using System;
using System.Collections.Generic;
using Polly.Utilities;

namespace Polly.CircuitBreaker
{
    class RollingHealthMetrics : IHealthMetrics
    {
        readonly long _samplingDuration;
        readonly long _windowDuration;
        readonly Queue<HealthCount> _windows;

        HealthCount _currentWindow;

        public RollingHealthMetrics(TimeSpan samplingDuration, short numberOfWindows)
        {
            _samplingDuration = samplingDuration.Ticks;

            _windowDuration = _samplingDuration/numberOfWindows;
            _windows = new Queue<HealthCount>(numberOfWindows + 1);
        }

        public void IncrementSuccess_NeedsLock()
        {
            ActualiseCurrentMetric_NeedsLock();

            _currentWindow.Successes++;
        }

        public void IncrementFailure_NeedsLock()
        {
            ActualiseCurrentMetric_NeedsLock();

            _currentWindow.Failures++;
        }

        public void Reset_NeedsLock()
        {
            _currentWindow = null;
            _windows.Clear();
        }

        public HealthCount GetHealthCount_NeedsLock()
        {
            ActualiseCurrentMetric_NeedsLock();

            var successes = 0;
            var failures = 0;
            foreach (var window in _windows)
            {
                successes += window.Successes;
                failures += window.Failures;
            }

            return new HealthCount
                   {
                       Successes = successes,
                       Failures = failures,
                       StartedAt = _windows.Peek().StartedAt
                   };
        }

        void ActualiseCurrentMetric_NeedsLock()
        {
            var now = SystemClock.UtcNow().Ticks;
            if ((_currentWindow == null) || (now - _currentWindow.StartedAt >= _windowDuration))
            {
                _currentWindow = new HealthCount {StartedAt = now};
                _windows.Enqueue(_currentWindow);
            }

            while ((_windows.Count > 0) && (now - _windows.Peek().StartedAt >= _samplingDuration))
                _windows.Dequeue();
        }
    }
}
