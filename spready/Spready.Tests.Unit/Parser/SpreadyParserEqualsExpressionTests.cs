using System.IO;
using NUnit.Framework;
using Spready.Parser;

namespace Spready.Tests.Unit.Parser
{
    [TestFixture]
    public class SpreadyParserEqualsExpressionTests : SpreadyFixture
    {
        protected override string InputFilename { get { return "Equals.txt"; } }

        [Test]
        public void ParseEqualsSourceFileReturnsSuccess()
        {
            var parser = new SpreadyParser();
            var result = parser.Parse(InputPath);
            Assert.That(result.Status, Is.EqualTo(ParseStatus.Success));
        }
    }
}
