namespace Spready.Calc
{
    public class CellBackingText : CellBacking
    {
        private string text;

        public CellBackingText(string theText)
        {
            this.text = theText;
        }

        public override string GetText()
        {
            return this.text;
        }
    }
}