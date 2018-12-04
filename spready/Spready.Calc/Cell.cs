using System;

namespace Spready.Calc
{
    /// <summary>
    /// A cell inside a spreadsheet worksheet.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Initializes a cell at <see cref="theLocation"/> with <see cref="theBacking"/> data.
        /// </summary>
        /// <param name="theLocation">Location of the cell inside the worksheet.</param>
        /// <param name="theBacking">Cell backing data.</param>
        public Cell(CellCoordinates theLocation, CellBacking theBacking)
        {
            if (theLocation == null)
                throw new ArgumentNullException(nameof(theLocation));
            if (theBacking == null)
                throw new ArgumentNullException(nameof(theBacking));

            Location = theLocation;
            Backing = theBacking;
            Display = CellDisplay.Default;
        }

        /// <summary>
        /// Initializes a cell at <see cref="theCellReferenceExpression"/> with <see cref="theBacking"/> data.
        /// </summary>
        /// <param name="theCellReferenceExpression">Expression for the cell location inside the worksheet.</param>
        /// <param name="theBacking">Cell backing data.</param>
        public Cell(string theCellReferenceExpression, CellBacking theBacking)
        {
            var theCellReference = new CellReference(theCellReferenceExpression);
            Location = theCellReference.GetCoordinates();
            Backing = theBacking;
        }

        /// <summary>
        /// Initializes a cell at <see cref="theLocation"/> with default backing data.
        /// </summary>
        /// <param name="theLocation">Location of the cell inside the worksheet.</param>
        public Cell(CellCoordinates theLocation)
        {
            if (theLocation == null)
                throw new ArgumentNullException(nameof(theLocation));

            Location = theLocation;
            Backing = CellBacking.Empty;
            Display = CellDisplay.Default;
        }

        /// <summary>
        /// Initializes a cell at <see cref="theCellReferenceExpression"/> with default no backing data.
        /// </summary>
        /// <param name="theCellReferenceExpression">Expression for the cell location inside the worksheet.</param>
        public Cell(string theCellReferenceExpression)
        {
            var theCellReference = new CellReference(theCellReferenceExpression);
            Location = theCellReference.GetCoordinates();
        }

        /// <summary>
        /// Gets the cell location inside a worksheet.
        /// </summary>
        public CellCoordinates Location { get; }

        /// <summary>
        /// Gets the cell backing.
        /// </summary>
        public CellBacking Backing { get; private set; }

        /// <summary>
        /// Gets the cell display.
        /// </summary>
        public CellDisplay Display { get; }

        /// <summary>
        /// Gets the text displayed inside the cell in the worksheet display.
        /// </summary>
        public string Text { get { return Backing.GetText(); } }

        /// <summary>
        /// Update the content of the cell.
        /// </summary>
        /// <param name="theBacking">New cell backing.</param>
        public void UpdateBacking(CellBacking theBacking)
        {
            if (theBacking == null)
                throw new ArgumentNullException(nameof(theBacking));

            Backing = theBacking;
        }
    }
}