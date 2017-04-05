using System.IO;
using NUnit.Framework;
using Spready.Parser;

namespace Spready.Tests.Unit.Parser
{
    [TestFixture]
    public class SpreadyParserWithTestsShould : SpreadyFixture
    {
        protected override string InputFilename { get { return "Tests.txt"; } }

        [Test]
        public void ParseFileReturnsSuccess()
        {
            var parser = new SpreadyParser();
            var result = parser.Parse(InputPath);
            Assert.That(result.Status, Is.EqualTo(ParseStatus.Success));
        }
    }
}
