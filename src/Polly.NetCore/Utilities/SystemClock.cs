using System;
using System.Threading;
using System.Threading.Tasks;

namespace Polly.Utilities
{
    /// <summary>
    ///     Time related delegates used to improve testability of the code
    /// </summary>
    public static class SystemClock
    {
        /// <summary>
        ///     Allows the setting of a custom Thread.Sleep implementation for testing.
        ///     By default this will be a call to <see cref="M:Thread.Sleep" />
        /// </summary>
        public static Action<TimeSpan> Sleep = Thread.Sleep;

        /// <summary>
        ///     Allows the setting of a custom async Sleep implementation for testing.
        ///     By default this will be a call to <see cref="M:Task.Delay" />
        /// </summary>
        public static Func<TimeSpan, CancellationToken, Task> SleepAsync = Task.Delay;

        /// <summary>
        ///     Allows the setting of a custom DateTime.UtcNow implementation for testing.
        ///     By default this will be a call to <see cref="DateTime.UtcNow" />
        /// </summary>
        public static Func<DateTime> UtcNow = () => DateTime.UtcNow;

        /// <summary>
        ///     Resets the custom implementations to their defaults.
        ///     Should be called during test teardowns.
        /// </summary>
        public static void Reset()
        {
            Sleep = Thread.Sleep;
            SleepAsync = Task.Delay;
            UtcNow = () => DateTime.UtcNow;
        }
    }
}
