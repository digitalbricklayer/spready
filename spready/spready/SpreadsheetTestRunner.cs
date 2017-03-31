using SpreadsheetLight;
using Spready.Parser;
using System;
using System.Collections.Generic;
using Spready.Nodes;

namespace Spready
{
    public class SpreadsheetTestRunner
    {
        private SLDocument spreadsheetUnderTest;

        public TestRunResult Run(TestSubOptions testOptions)
        {
            var inputFilename = testOptions.Input;

            // Parse the source code...
            var spreadyParser = new SpreadyParser();
            var parseResult = spreadyParser.Parse(inputFilename);
            if (parseResult.Status != ParseStatus.Success)
            {
                Console.Error.WriteLine("Error: syntax error in input file.");
                return TestRunResult.CreateBadSyntaxResult();
            }

            var theRootNode = parseResult.Root;

            // Compile the spreadsheet
            var compiler = new SpreadsheetCompiler();
            var compilationResult = compiler.Compile(new SpreadsheetDetails(inputFilename, theRootNode));

            if (!compilationResult.Status)
            {
                Console.Error.WriteLine("Error: compilation failed.");
                return TestRunResult.CreateFailedResult();
            }

            // Run the tests...
            var accumulatedGroupResults = new List<TestGroupResult>();

            using (this.spreadsheetUnderTest = new SLDocument(compilationResult.OutputPath))
            {
                foreach (var testGroupNode in theRootNode.TestGroupNodes)
                {
                    var testResult = ExecuteTestGroup(testGroupNode);
                    accumulatedGroupResults.Add(testResult);
                }
            }

            return TestRunResult.CreateFromTestResults(accumulatedGroupResults.AsReadOnly());
        }

        private TestGroupResult ExecuteTestGroup(TestGroupNode testGroupNode)
        {
            var accumulatedTestResults = new List<TestResult>();

            this.spreadsheetUnderTest.SelectWorksheet(testGroupNode.WorksheetName);
            foreach (var testNode in testGroupNode.Tests.Tests)
            {
                var testResult = ExecuteTest(testNode);
            }

            return new TestGroupResult(accumulatedTestResults);
        }

        private TestResult ExecuteTest(TestNode testNode)
        {
            var result = ExecuteExpression(testNode.Expression);

            if (result)
            {
                Console.Out.Write(".");
                return TestResult.Passed(testNode.TestName.Name);
            }
            else
            {
                Console.Out.Write("*");
                return TestResult.Failed(testNode.TestName.Name);
            }
        }

        private bool ExecuteExpression(TestExpressionNode expression)
        {
            switch (expression.Statement.InnerExpression)
            {
                case WorksheetExpressionNode worksheetExpression:
                    return this.spreadsheetUnderTest.IsWorksheetHidden(worksheetExpression.WorksheetName.Name);

                case CellExpressionNode cellExpressionNode:
                    break;

                default:
                    throw new NotImplementedException("Unknown test expression.");
            }

            return false;
        }
    }
}