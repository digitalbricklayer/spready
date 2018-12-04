using NUnit.Framework;

namespace Spready.Calc.Tests.Unit
{
    [TestFixture]
    public class CellReferenceShould
    {
        [Test]
        public void ParseCellReference()
        {
            var sut = new CellReference("A1");
            var actualPoint = sut.GetCoordinates();
            Assert.That(actualPoint, Is.EqualTo(new CellCoordinates(1, 1)));
        }

        [Test]
        public void ParseAnotherCellReference()
        {
            var sut = new CellReference("AA1");
            var actualPoint = sut.GetCoordinates();
            Assert.That(actualPoint, Is.EqualTo(new CellCoordinates(27,1)));
        }
    }
}
