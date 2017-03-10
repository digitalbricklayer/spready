using System.IO;
using NUnit.Framework;
using Spready.Parser;

namespace Spready.Tests.Unit
{
    [TestFixture]
    public class SpreadyParserEqualsExpressionTests
    {
        private const string InputFilename = "Equals.spready";
        private const string OutputFilename = "Equals.xlsx";
        private const string SourceCode = @"worksheet Sheet1 { A1 10, A2 20, A3 =SUM(A1,A2) }";
        private string inputPath;
        private string outputPath;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            inputPath = Path.Combine(Path.GetTempPath(), InputFilename);
            File.WriteAllText(inputPath, SourceCode);
            outputPath = Path.Combine(Path.GetTempPath(), OutputFilename);
        }

        [OneTimeTearDown]
        public void OneTimeCleanup()
        {
            File.Delete(inputPath);
            File.Delete(outputPath);
        }

        [Test]
        public void ParseEqualsSourceFileReturnsSuccess()
        {
            var parser = new SpreadyParser();
            var result = parser.Parse(inputPath);
            Assert.That(result.Status, Is.EqualTo(ParseStatus.Success));
        }
    }
}
