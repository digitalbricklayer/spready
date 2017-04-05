using System.IO;
using NUnit.Framework;
using Spready.Parser;

namespace Spready.Tests.Unit.Parser
{
    [TestFixture]
    public class SpreadyParserMultiWorksheetTests : SpreadyFixture
    {
        protected override string InputFilename { get { return "MultipleWorksheets.txt"; } }

        [Test]
        public void ParseSourceFileReturnsSuccess()
        {
            var parser = new SpreadyParser();
            var result = parser.Parse(InputPath);
            Assert.That(result.Status, Is.EqualTo(ParseStatus.Success));
        }
    }
}
