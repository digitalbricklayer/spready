using System.IO;
using NUnit.Framework;
using Spready.Parser;

namespace Spready.Tests.Unit
{
    [TestFixture]
    public class SpreadyParserMultiWorksheetTests
    {
        private const string InputFilename = "MultipleWorksheets.spready";
        private const string OutputFilename = "MultipleWorksheets.xlsx";
        private const string SourceCode = @"worksheet Sheet1 { A1 10, A2 20 } worksheet Sheet2 { A1=SUM(Sheet1!A1, Sheet1!A2) }";
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
