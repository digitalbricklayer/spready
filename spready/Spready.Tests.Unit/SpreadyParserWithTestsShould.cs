using System.IO;
using NUnit.Framework;
using Spready.Parser;
using System.Reflection;
using System;

namespace Spready.Tests.Unit
{
    [TestFixture]
    public class SpreadyParserWithTestsShould
    {
        private readonly string InputFilename = "Tests.txt";
        private readonly string OutputFilename = "Tests.xlsx";
        private string sourceCode;
        private string inputPath;
        private string outputPath;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Spready.Tests.Unit.Sources.Tests.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                sourceCode = reader.ReadToEnd();
            }
            inputPath = Path.Combine(Path.GetTempPath(), InputFilename);
            File.WriteAllText(inputPath, sourceCode);
            outputPath = Path.Combine(Path.GetTempPath(), OutputFilename);
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
