using System.IO;
using NUnit.Framework;
using Spready.Parser;

namespace Spready.Tests.Unit
{
    [TestFixture]
    public class SpreadyParserSimpleExpressionTests
    {
        private const string InputFilename = "simples.spready";
        private const string OutputFilename = "simples.xlsx";
        private const string SourceCode = @"""Sheet1"" { A1 10, A2 ""Jack"" }";
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
        public void ParseSourceFileReturnsSuccess()
        {
            var parser = new SpreadyParser();
            var result = parser.Parse(inputPath);
            Assert.That(result.Status, Is.EqualTo(ParseStatus.Success));
        }
    }
}
