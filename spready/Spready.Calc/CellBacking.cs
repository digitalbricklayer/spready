namespace Spready.Calc
{
    /// <summary>
    /// Cell backing data.
    /// </summary>
    /// <remarks>
    /// The backing data is the raw information entered into the cell. It may
    /// be formatted to look differently when displayed in the cell.
    /// </remarks>
    public abstract class CellBacking
    {
        /// <summary>
        /// Get an empty cell contents.
        /// </summary>
        public static CellBacking Empty => new CellBackingEmpty();

        /// <summary>
        /// Get the content of the cell as a string.
        /// </summary>
        /// <returns>A string representation of the cell content.</returns>
        public abstract string GetText();
    }
}
