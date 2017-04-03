using NUnit.Framework;
using Spready.Parser;

namespace Spready.Tests.Unit
{
    [TestFixture]
    public class SpreadyParserCommentTests : SpreadyFixture
    {
        protected override string InputFilename { get { return "Comment.txt"; } }

        [Test]
        public void ParseCommentSourceFileReturnsSuccess()
        {
            var parser = new SpreadyParser();
            var result = parser.Parse(InputPath);
            Assert.That(result.Status, Is.EqualTo(ParseStatus.Success));
        }
    }
}
