namespace Polly.CircuitBreaker
{
    interface IHealthMetrics
    {
        void IncrementSuccess_NeedsLock();
        void IncrementFailure_NeedsLock();

        void Reset_NeedsLock();

        HealthCount GetHealthCount_NeedsLock();
    }
}
