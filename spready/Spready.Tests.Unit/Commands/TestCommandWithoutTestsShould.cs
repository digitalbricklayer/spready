using NUnit.Framework;
using Spready.Commands;

namespace Spready.Tests.Unit.Commands
{
    [TestFixture]
    public class TestCommandWithoutTestsShould : SpreadyFixture
    {
        protected override string InputFilename { get { return "HiddenWorksheet.txt"; } }

        [Test]
        public void ExitCodeReturnsSuccess()
        {
            var testCommand = new TestCommand();
            var exitCode = testCommand.Run(new TestSubOptions { Input = InputPath });
            Assert.That(exitCode, Is.EqualTo(0));
        }
    }
}
