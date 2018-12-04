using System.Collections.Generic;

namespace Spready.Calc
{
    public class Spreadsheet
    {
        private readonly List<Worksheet> worksheets;

        public IList<Worksheet> Worksheets => worksheets.AsReadOnly();

        public Spreadsheet()
        {
            worksheets = new List<Worksheet>();
        }

        public void AddWorksheet(Worksheet newWorksheet)
        {
            this.worksheets.Add(newWorksheet);
        }

        public void RemoveWorksheet(Worksheet theWorksheet)
        {
            this.worksheets.Remove(theWorksheet);
        }
    }
}
