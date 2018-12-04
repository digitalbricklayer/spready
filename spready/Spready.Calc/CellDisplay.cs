namespace Spready.Calc
{
    public class CellDisplay
    {
        public static CellDisplay Default => new CellDisplay();

        public CellDisplay()
        {
            Value = string.Empty;
        }

        public string Value { get; }
    }
}