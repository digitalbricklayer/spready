using System;
using VDS.Common.Collections;

namespace Spready.Calc
{
    public class Worksheet
    {
        public const int MaxColumns = 256;
        public const int MaxRows = 64_536;
        public const string DefaultWorksheetName = "Sheet1";

        private readonly BinarySparseArray<Cell> cells;

        public Worksheet(string theWorksheetName)
        {
            if (string.IsNullOrWhiteSpace(theWorksheetName))
                throw new ArgumentException(nameof(theWorksheetName));

            Name = theWorksheetName;
            this.cells = new BinarySparseArray<Cell>(MaxColumns * MaxRows);
        }

        public Worksheet()
        {
            Name = DefaultWorksheetName;
            this.cells = new BinarySparseArray<Cell>(MaxColumns * MaxRows);
        }

        public string Name { get; set; }

        public Cell GetAt(CellReference theCellReference)
        {
            if (theCellReference == null)
                throw new ArgumentNullException(nameof(theCellReference));

            var theCellCoordinates = theCellReference.GetCoordinates();
            var index = GetIndexFrom(theCellCoordinates);
            return this.cells[index];
        }

        public Cell GetAt(string theCellReferenceExpression)
        {
            return GetAt(new CellReference(theCellReferenceExpression));
        }

        public void SetAt(Cell theCell)
        {
            if (theCell == null)
                throw new ArgumentNullException(nameof(theCell));

            var index = GetIndexFrom(theCell.Location);
            this.cells[index] = theCell;
        }

        private static int GetIndexFrom(CellCoordinates theCellCoordinates)
        {
            return (theCellCoordinates.Column - 1) * MaxColumns + theCellCoordinates.Row - 1;
        }
    }
}
