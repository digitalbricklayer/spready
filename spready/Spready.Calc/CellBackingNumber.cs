using System;

namespace Spready.Calc
{
    public class CellBackingNumber : CellBacking
    {
        private int number;

        public CellBackingNumber(int value)
        {
            this.number = value;
        }

        /// <inheritdoc />
        public override string GetText()
        {
            return Convert.ToString(this.number);
        }
    }
}