using NUnit.Framework;
using Spready.Parser;

namespace Spready.Tests.Unit
{
    [TestFixture]
    public class SpreadyParserSimpleExpressionTests : SpreadyFixture
    {
        protected override string InputFilename { get { return "Simple.txt"; } }

        [Test]
        public void ParseSimpleSourceFileReturnsSuccess()
        {
            var parser = new SpreadyParser();
            var result = parser.Parse(InputPath);
            Assert.That(result.Status, Is.EqualTo(ParseStatus.Success));
        }
    }
}
