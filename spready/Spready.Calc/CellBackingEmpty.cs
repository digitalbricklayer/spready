using System;

namespace Spready.Calc
{
    public class CellBackingEmpty : CellBacking
    {
        /// <inheritdoc />
        public override string GetText()
        {
            return String.Empty;
        }
    }
}
