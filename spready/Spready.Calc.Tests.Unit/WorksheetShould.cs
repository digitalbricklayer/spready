using System;
using NUnit.Framework;

namespace Spready.Calc.Tests.Unit
{
    [TestFixture]
    public class WorksheetShould
    {
        [Test]
        public void GetCellReturnsCell([Values("A1", "B2", "A3", "BR3762")] string theCellReference)
        {
            var sut = new Worksheet();
            sut.SetAt(new Cell(theCellReference));
            var theCell = sut.GetAt(theCellReference);
            Assert.That(theCell, Is.Not.Null);
        }

        [Test]
        public void AddStringCellReturnsCellWithExpectedText()
        {
            var sut = new Worksheet();
            sut.SetAt(new Cell("A1", new CellBackingText("Hello")));
            var theCell = sut.GetAt("A1");
            Assert.That(theCell.Text, Is.EqualTo("Hello"));
        }

        [Test]
        public void AddBoolCellReturnsCellWithExpectedText()
        {
            var sut = new Worksheet();
            sut.SetAt(new Cell("A1", new CellBackingBoolean(true)));
            var theCell = sut.GetAt("A1");
            Assert.That(theCell.Text, Is.EqualTo("True"));
        }

        [Test]
        public void AddNumberCellReturnsCellWithExpectedText()
        {
            var sut = new Worksheet();
            sut.SetAt(new Cell("A1", new CellBackingNumber(int.MaxValue)));
            var theCell = sut.GetAt("A1");
            Assert.That(theCell.Text, Is.EqualTo(Convert.ToString(int.MaxValue)));
        }

        [Test]
        public void AddExpressionCellReturnsCellWithExpectedText()
        {
            var sut = new Worksheet();
            sut.SetAt(new Cell("A1", new CellBackingExpression("=Sum(A1,A2)")));
            var theCell = sut.GetAt("A1");
            Assert.That(theCell.Text, Is.EqualTo("=Sum(A1,A2)"));
        }
    }
}
