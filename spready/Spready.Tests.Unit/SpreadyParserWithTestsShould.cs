using System.IO;
using NUnit.Framework;
using Spready.Parser;

namespace Spready.Tests.Unit
{
    [TestFixture]
    public class SpreadyParserWithTestsShould : SpreadyFixture
    {
        private readonly string InputFilename = "Tests.txt";
        private string inputPath;
        private string outputPath;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            (inputPath, outputPath) = WriteSourcesFrom("Spready.Tests.Unit.Sources.Tests.txt", InputFilename);
        }

        [OneTimeTearDown]
        public void OneTimeCleanup()
        {
            File.Delete(inputPath);
            File.Delete(outputPath);
        }

        [Test]
        public void ParseFileReturnsSuccess()
        {
            var parser = new SpreadyParser();
            var result = parser.Parse(inputPath);
            Assert.That(result.Status, Is.EqualTo(ParseStatus.Success));
        }
    }
}
