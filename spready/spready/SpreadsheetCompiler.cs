using System.IO;
using Irony.Interpreter.Ast;
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
                    // Add sheet contents
                    foreach (var expressionNode in worksheetNode.Expressions)
                    {
                        if (expressionNode.CellReference.CellReference is InternalSheetCellReferenceNode)
                        {
                            var internalCellReference = (InternalSheetCellReferenceNode) expressionNode.CellReference.CellReference;
                            if (expressionNode.CellValue.Value is CellNumberNode)
                            {
                                var number = (CellNumberNode)expressionNode.CellValue.Value;
                                newSpreadsheet.SetCellValue(internalCellReference.CellName, number.Value);
                            }
                            else if (expressionNode.CellValue.Value is CellStringNode)
                            {
                                var number = (CellStringNode)expressionNode.CellValue.Value;
                                newSpreadsheet.SetCellValue(internalCellReference.CellName, number.Value);
                            }
                        }
                    }
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
