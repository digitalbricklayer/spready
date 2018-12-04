namespace Spready.Calc
{
    public class CellBackingBoolean : CellBacking
    {
        private bool content;

        public CellBackingBoolean(bool value)
        {
            this.content = value;
        }

        /// <inheritdoc />
        public override string GetText()
        {
            return this.content ? "True" : "False";
        }
    }
}