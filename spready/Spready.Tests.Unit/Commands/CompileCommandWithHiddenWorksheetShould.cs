using NUnit.Framework;
using SpreadsheetLight;
using Spready.Commands;

namespace Spready.Tests.Unit.Commands
{
    [TestFixture]
    public class CompileCommandWithHiddenWorksheetShould : SpreadyFixture
    {
        protected override string InputFilename
        {
            get
            {
                return "HiddenWorksheet.txt";
            }
        }

        [Test]
        public void CreateSpreadsheetWithHiddenWorksheet()
        {
            var compileCommand = new CompileCommand();
            compileCommand.Run(CreateCompileSubOptions());
            using (var spreadsheet = new SLDocument(OutputPath))
            {
                var isBackingWorksheetHidden = spreadsheet.IsWorksheetHidden("Backing");
                Assert.That(isBackingWorksheetHidden, Is.True);
            }
        }

        [Test]
        public void ExitCodeReturnsSuccess()
        {
            var compileCommand = new CompileCommand();
            var exitCode = compileCommand.Run(CreateCompileSubOptions());
            Assert.That(exitCode, Is.EqualTo(0));
        }

        private CompileSubOptions CreateCompileSubOptions()
        {
            return new CompileSubOptions { Input = InputPath };
        }
    }
}
