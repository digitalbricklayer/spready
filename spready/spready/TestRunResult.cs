using System;
using System.Collections.Generic;
using System.Linq;

namespace Spready
{
    public class TestRunResult
    {
        public TestRunStatus Status { get; private set; }
        public IList<TestGroupResult> Results { get; private set; }

        private TestRunResult(IReadOnlyCollection<TestGroupResult> accumulatedTestResults)
        {
            Results = new List<TestGroupResult>(accumulatedTestResults);
            Status = GetStatusFrom(accumulatedTestResults);
        }

        private TestRunResult(TestRunStatus runStatus)
        {
            Status = runStatus;
            Results = new List<TestGroupResult>();
        }

        public static TestRunResult CreateBadSyntaxResult()
        {
            return new TestRunResult(TestRunStatus.BadSyntax);
        }

        public static TestRunResult CreateFailedResult()
        {
            return new TestRunResult(TestRunStatus.Fail);
        }

        public int GetTotalTests()
        {
            var counter = 0;
            foreach (var groupResult in Results)
            {
                counter += groupResult.GetTotalTests();
            }

            return counter;
        }

        public int GetFailedTestTotal()
        {
            var counter = 0;
            foreach (var groupResult in Results)
            {
                counter += groupResult.GetFailedTotalTests();
            }

            return counter;
        }

        public bool IsRunSuccessful
        {
            get
            {
                return Status == TestRunStatus.Success;
            }
        }

        public static TestRunResult CreateFromTestResults(IReadOnlyCollection<TestGroupResult> accumulatedTestResults)
        {
            return new TestRunResult(accumulatedTestResults);
        }

        private TestRunStatus GetStatusFrom(IReadOnlyCollection<TestGroupResult> accumulatedTestResults)
        {
            if (accumulatedTestResults.Any(result => result.GetFailedTotalTests() > 0))
                return TestRunStatus.Fail;

            return TestRunStatus.Success;
        }
    }
}