using System.IO;
using NUnit.Framework;
using Spready.Commands;

namespace Spready.Tests.Unit
{
    [TestFixture]
    public class TestCommandWithoutTestsShould
    {
        private readonly string InputFilename = "HiddenWorksheet.spready";
        private readonly string OutputFilename = "HiddenWorksheet.xlsx";
        private readonly string SourceCode = @"worksheet Summary { A1=SUM(Backing!A1, Backing!A2) } worksheet Backing hidden { 	/* Hidden worksheet. */ A1 10, A2 20 }";
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
        public void ExitCodeReturnsSuccess()
        {
            var testCommand = new TestCommand();
            var exitCode = testCommand.Run(new TestSubOptions { Input = inputPath });
            Assert.That(exitCode, Is.EqualTo(0));
        }
    }
}
