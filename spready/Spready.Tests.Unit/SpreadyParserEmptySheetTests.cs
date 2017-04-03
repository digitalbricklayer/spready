using System.IO;
using NUnit.Framework;
using Spready.Parser;

namespace Spready.Tests.Unit
{
    [TestFixture]
    public class SpreadyParserEmptySheetTests : SpreadyFixture
    {
        protected override string InputFilename { get { return "Empty.txt"; } }

        [Test]
        public void ParseEmptySheetSourceFileReturnsSuccess()
        {
            var parser = new SpreadyParser();
            var result = parser.Parse(InputPath);
            Assert.That(result.Status, Is.EqualTo(ParseStatus.Success));
        }
    }
}
