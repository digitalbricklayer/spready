using System.Linq;
using System.Collections.Generic;

namespace Spready
{
    public class TestGroupResult
    {
        public IList<TestResult> Results { get; private set; }

        public TestGroupResult(IReadOnlyCollection<TestResult> accumulatedTestResults)
        {
            Results = new List<TestResult>(accumulatedTestResults);
        }

        public int GetTotalTests()
        {
            return Results.Count;
        }

        public int GetFailedTotalTests()
        {
            return Results.Count(_ => _.Status != true);
        }
    }
}