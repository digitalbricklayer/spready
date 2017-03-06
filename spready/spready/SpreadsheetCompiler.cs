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
                    foreach (var expressionNode in worksheetNode.Statements)
                    {
                        if (expressionNode is SimpleStatementNode)
                        {
                            var simpleStatementNode = (SimpleStatementNode) expressionNode;
                            if (simpleStatementNode.CellReference.CellReference is LocalSheetCellReferenceNode)
                            {
                                var internalCellReference = (LocalSheetCellReferenceNode)simpleStatementNode.CellReference.CellReference;
                                if (simpleStatementNode.CellValue.Value is CellNumberNode)
                                {
                                    var number = (CellNumberNode)simpleStatementNode.CellValue.Value;
                                    newSpreadsheet.SetCellValue(internalCellReference.CellName, number.Value);
                                }
                                else if (simpleStatementNode.CellValue.Value is CellStringNode)
                                {
                                    var number = (CellStringNode)simpleStatementNode.CellValue.Value;
                                    newSpreadsheet.SetCellValue(internalCellReference.CellName, number.Value);
                                }
                            }
                        }
                        else if (expressionNode is EqualsStatementNode)
                        {
                            var equalsStatementNode = (EqualsStatementNode) expressionNode;
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
