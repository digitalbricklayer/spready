using System;
using System.IO;
using System.Text;
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
                }
                newSpreadsheet.DeleteWorksheet(TemporaryWorksheetName);
                newSpreadsheet.SaveAs(GetOutputFileFrom(inputFilename));
            }
        }

        private void ProcessEqualsStatement(SLDocument newSpreadsheet, EqualsStatementNode equalsStatementNode)
        {
            var assignTo = (CellReferenceNode)equalsStatementNode.AssignTo;
            switch (assignTo.CellReference)
            {
                case LocalSheetCellReferenceNode internalCellReference:
                    newSpreadsheet.SetCellValue(internalCellReference.CellName,
                                                GenerateStringExpressionFrom(equalsStatementNode));
                    break;
            }
        }

        private static void ProcessSimpleStatement(SLDocument newSpreadsheet, SimpleStatementNode simpleStatementNode)
        {
            switch (simpleStatementNode.CellReference.CellReference)
            {
                case LocalSheetCellReferenceNode internalCellReference:
                    switch (simpleStatementNode.CellValue.Value)
                    {
                        case CellNumberNode numberValue:
                            newSpreadsheet.SetCellValue(internalCellReference.CellName, numberValue.Value);
                            break;

                        case CellStringNode stringValue:
                            newSpreadsheet.SetCellValue(internalCellReference.CellName, stringValue.Value);
                            break;
                    }
                    break;

                default:
                    throw new NotImplementedException("Cell refernce not implemented.");
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

        private string GenerateCellReferenceTextFrom(XNode cellReferenceNode)
        {
            return cellReferenceNode.GetFullName();
        }

        private string GetOutputFileFrom(string inputFilename)
        {
            return Path.GetFileNameWithoutExtension(inputFilename) + ".xlsx";
        }
    }
}
