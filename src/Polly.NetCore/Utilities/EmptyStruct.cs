namespace Polly.Utilities
{
    /// <summary>
    ///     A null struct for policies and actions which do not return a TResult.
    /// </summary>
    struct EmptyStruct
    {
        internal static readonly EmptyStruct Instance = new EmptyStruct();
    }
}
