using System.IO;
using NUnit.Framework;
using Spready.Parser;

namespace Spready.Tests.Unit
{
    [TestFixture]
    public class SpreadyParserEmptySheetTests
    {
        private readonly string InputFilename = "Empty.spready";
        private readonly string OutputFilename = "Empty.xlsx";
        private readonly string SourceCode = @"worksheet Sheet1 { }";
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
        public void ParseEmptySheetSourceFileReturnsSuccess()
        {
            var parser = new SpreadyParser();
            var result = parser.Parse(inputPath);
            Assert.That(result.Status, Is.EqualTo(ParseStatus.Success));
        }
    }
}
