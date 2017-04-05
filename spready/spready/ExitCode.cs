namespace Spready
{
    enum ExitCode : int
    {
        Success = 0,

        /// <summary>
        /// Missing input file.
        /// </summary>
        InvalidInput = 1,
        
        /// <summary>
        /// Input file syntax is invalid.
        /// </summary>
        BadSyntax = 2,

        /// <summary>
        /// Invalid command line arguments.
        /// </summary>
        BadArguments = 3,
        
        /// <summary>
        /// One or more tests failed.
        /// </summary>
        Fail
    }
}
