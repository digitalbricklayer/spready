using System;

namespace Spready
{
    public class TestResult
    {
        public bool Status { get; private set; }
        public string TestName { get; private set; }

        public TestResult(string testName)
        {
            if (string.IsNullOrWhiteSpace(testName))
                throw new ArgumentException(nameof(testName));

            TestName = testName;
        }

        public static TestResult Passed(string testName)
        {
            return new TestResult(testName) { Status = true };
        }

        public static TestResult Failed(string testName)
        {
            return new TestResult(testName) { Status = false };
        }
    }
}