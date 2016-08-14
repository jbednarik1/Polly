using System.Threading;
using System.Threading.Tasks;

namespace Polly.Retry
{
    partial interface IRetryPolicyState<TResult>
    {
        Task<bool> CanRetryAsync(DelegateResult<TResult> delegateResult, CancellationToken ct, bool continueOnCapturedContext);
    }
}
