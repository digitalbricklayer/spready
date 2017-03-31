namespace Spready
{
    public class CompileResult
    {
        public string OutputPath { get; private set; }
        public bool Status { get; private set; }

        private CompileResult()
        {
            OutputPath = string.Empty;
        }

        public static CompileResult CreateFailResult()
        {
            return new CompileResult { Status = false };
        }

        public static CompileResult CreateSuccessfulResult(string outputPath)
        {
            return new CompileResult { Status = true, OutputPath = outputPath };
        }
    }
}