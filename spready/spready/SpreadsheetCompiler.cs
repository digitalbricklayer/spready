using System;
using System.IO;
using System.Text;
using SpreadsheetLight;
using Spready.Nodes;
using System.Diagnostics;

namespace Spready
{
    public class SpreadsheetCompiler
    {
        private const string TemporaryWorksheetName = "sheet to be deleted";

        public CompileResult Compile(SpreadsheetDetails spreadsheetDetails)
        {
            var theRootNode = spreadsheetDetails.RootNode;

            // Create the spreadsheet
            using (var newSpreadsheet = new SLDocument())
            {
                // Remove the default worksheet
                newSpreadsheet.AddWorksheet(TemporaryWorksheetName);
                newSpreadsheet.DeleteWorksheet(SLDocument.DefaultFirstSheetName);
                foreach (var worksheetNode in theRootNode.WorksheetNodes)
                {
                    var addStatus = newSpreadsheet.AddWorksheet(worksheetNode.Name);

                    if (!addStatus)
                    {
                        Console.Error.WriteLine("Error: failed to create new worksheet {1}", worksheetNode.Name);
                        return CompileResult.CreateFailResult();
                    }

                    // Add sheet contents
                    foreach (var expressionNode in worksheetNode.Statements)
                    {
                        switch (expressionNode)
                        {
                            case SimpleStatementNode simpleStatementNode:
                                ProcessSimpleStatement(newSpreadsheet, simpleStatementNode);
                                break;

                            case EqualsStatementNode equalsStatementNode:
                                ProcessEqualsStatement(newSpreadsheet, equalsStatementNode);
                                break;

                            default:
                                throw new NotImplementedException("Unknown statement type.");
                        }
                    }

                    if (!worksheetNode.HiddenAttribute.IsVisible)
                    {
                        newSpreadsheet.HideWorksheet(worksheetNode.Name);
                        Debug.Assert(newSpreadsheet.IsWorksheetHidden(worksheetNode.Name));
                    }
                }
                newSpreadsheet.DeleteWorksheet(TemporaryWorksheetName);
                SelectActiveWorksheet(newSpreadsheet);
                newSpreadsheet.SaveAs(GetOutputPathFrom(spreadsheetDetails.InputFilename));
            }

            return CompileResult.CreateSuccessfulResult(GetOutputPathFrom(spreadsheetDetails.InputFilename));
        }

        /// <summary>
        /// Select the first non-hidden worksheet.
        /// </summary>
        /// <param name="newSpreadsheet">Spreadsheet.</param>
        private void SelectActiveWorksheet(SLDocument newSpreadsheet)
        {
            var allSheetNames = newSpreadsheet.GetSheetNames();
            foreach (var sheetName in allSheetNames)
            {
                if (!newSpreadsheet.IsWorksheetHidden(sheetName))
                {
                    newSpreadsheet.SelectWorksheet(sheetName);
                    break;
                }
            }
        }

        private void ProcessEqualsStatement(SLDocument theSpreadsheet, EqualsStatementNode equalsStatementNode)
        {
            var assignTo = (CellReferenceNode)equalsStatementNode.AssignTo;
            switch (assignTo.CellReference)
            {
                case LocalSheetCellReferenceNode internalCellReference:
                    theSpreadsheet.SetCellValue(internalCellReference.CellName,
                                                GenerateStringExpressionFrom(equalsStatementNode));
                    break;

                default:
                    throw new NotImplementedException("Cell reference not implemented.");
            }
        }

        private static void ProcessSimpleStatement(SLDocument theSpreadsheet, SimpleStatementNode simpleStatementNode)
        {
            switch (simpleStatementNode.CellReference.CellReference)
            {
                case LocalSheetCellReferenceNode internalCellReference:
                    switch (simpleStatementNode.CellValue.Value)
                    {
                        case CellNumberNode numberValue:
                            theSpreadsheet.SetCellValue(internalCellReference.CellName, numberValue.Value);
                            break;

                        case CellStringNode stringValue:
                            theSpreadsheet.SetCellValue(internalCellReference.CellName, stringValue.Value);
                            break;
                    }
                    break;

                default:
                    throw new NotImplementedException("Cell reference not implemented.");
            }
        }

        private string GenerateStringExpressionFrom(EqualsStatementNode equalsStatementNode)
        {
            var functionCall = (FunctionCallNode) equalsStatementNode.FunctionCall;
            switch (functionCall.FunctionName)
            {
                case "SUM":
                    return "=SUM(" + GenerateStringFunctionArgumentsFrom(equalsStatementNode.FunctionCallArguments) + ")";

                default:
                    throw new NotImplementedException("Function call not implemented.");
            }
        }

        private string GenerateStringFunctionArgumentsFrom(FunctionCallArgumentNodeList functionCallArguments)
        {
            var functionArgsBuilder = new StringBuilder();
            foreach (var argument in functionCallArguments.Arguments)
            {
                functionArgsBuilder.Append(GenerateCellReferenceTextFrom(argument.CellReference) + ",");
            }

            var x = functionArgsBuilder.ToString();
            return x.TrimEnd(new[] { ',' });
        }

        private string GenerateCellReferenceTextFrom(CellReferenceBaseNode cellReferenceNode)
        {
            return cellReferenceNode.GetFullReference();
        }

        private string GetOutputPathFrom(string inputPath)
        {
            return Path.ChangeExtension(inputPath, ".xlsx");
        }
    }
}
