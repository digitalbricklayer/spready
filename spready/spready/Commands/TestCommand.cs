using System;

namespace Spready.Commands
{
    public class TestCommand : ICommand
    {
        public int Run(object options)
        {
            var testOptions = (TestSubOptions) options;
            var testRunner = new SpreadsheetTestRunner();
            var result = testRunner.Run(testOptions);

            if (result.Status == TestRunStatus.BadSyntax)
            {
                return (int)ExitCode.BadSyntax;
            }

            PrintReport(result);

            return (int)ExitCode.Success;
        }

        private void PrintReport(TestRunResult result)
        {
            if (result.IsRunSuccessful)
            {
                PrintSummary(result);
            }
            else
            {
                PrintFailureResport(result);
            }
        }

        private void PrintSummary(TestRunResult result)
        {
            Console.Out.WriteLine("Tests run successful: {0} tests", result.GetTotalTests());
        }

        private void PrintFailureResport(TestRunResult result)
        {
            Console.Out.WriteLine("Test run failed: total: {0}, failed {1}", result.GetTotalTests(), result.GetFailedTestTotal());
        }
    }
}
