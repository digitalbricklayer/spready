namespace Spready
{
    public enum TestRunStatus
    {
        /// <summary>
        /// All tests ran successfully.
        /// </summary>
        Success,
        /// <summary>
        /// Language could not be compiled.
        /// </summary>
        BadSyntax,
        /// <summary>
        /// One or more tests failed.
        /// </summary>
        Fail
    }
}