using System.IO;
using SpreadsheetLight;
using Spready.Nodes;

namespace Spready
{
    class SpreadsheetCompiler
    {
        private const string TemporaryWorksheetName = "sheet to be deleted";

        internal void Compile(SpreadyNode theRootNode, string inputFilename)
        {
            // Create the spreadsheet
            using (var newSpreadsheet = new SLDocument())
            {
                // Remove the default worksheet
                newSpreadsheet.AddWorksheet(TemporaryWorksheetName);
                newSpreadsheet.DeleteWorksheet("Sheet1");
                foreach (var worksheetNode in theRootNode.WorksheetNodes)
                {
                    newSpreadsheet.AddWorksheet(worksheetNode.Name);
                }
                newSpreadsheet.DeleteWorksheet(TemporaryWorksheetName);
                newSpreadsheet.SaveAs(GetOutputFileFrom(inputFilename));
            }
        }

        private string GetOutputFileFrom(string inputFilename)
        {
            return Path.GetFileNameWithoutExtension(inputFilename) + ".xlsx";
        }
    }
}
