namespace Polly.Retry
{
    partial interface IRetryPolicyState<TResult>
    {
        bool CanRetry(DelegateResult<TResult> delegateResult);
    }
}
