namespace Spready.Calc
{
    public class CellBackingExpression : CellBacking
    {
        private string expression;

        public CellBackingExpression(string theExpression)
        {
            this.expression = theExpression;
        }

        /// <inheritdoc />
        public override string GetText()
        {
            return this.expression;
        }
    }
}
