using Irony.Interpreter.Ast;
using Irony.Parsing;
using Spready.Nodes;

namespace Spready.Grammar
{
    [Language("Spready", "0.1", "A grammar for a spreadsheet language.")]
    internal class SpreadyGrammar : Irony.Parsing.Grammar
    {
        public SpreadyGrammar()
            : base(caseSensitive: false)
        {
            LanguageFlags = LanguageFlags.CreateAst |
                            LanguageFlags.NewLineBeforeEOF;

            // Terminals
            var worksheetName = new StringLiteral("worksheet name", "\"", StringOptions.NoEscapes, typeof(WorksheetNameNode));
            var cellValueNumber = new NumberLiteral("cell number", NumberOptions.None, typeof(CellNumberNode));
            var cellValueString = new StringLiteral("cell string", "\"", StringOptions.NoEscapes, typeof(CellStringNode));
            var cellName = new RegexBasedTerminal("cell name", @"[A-Za-z]\w*[0-9]\w*");
            cellName.AstConfig.NodeType = typeof (CellNameNode);

            // Non-termninals
            var worksheet = new NonTerminal("worksheet", typeof(WorksheetNode));
            var worksheetList = new NonTerminal("worksheet list", typeof(SpreadyNode));
            var expression = new NonTerminal("expression", typeof(ExpressionNode));
            var expressionList = new NonTerminal("expression list", typeof(ExpressionListNode));
            var cellReference = new NonTerminal("cell reference", typeof(CellReferenceNode));
            var cellValue = new NonTerminal("cell value", typeof(CellValueNode));
            var internalSheetCellReference = new NonTerminal("internal sheet cell reference", typeof(InternalSheetCellReferenceNode));
            var infraSheetCellReference = new NonTerminal("infra sheet cell reference", typeof(InfraSheetCellReferenceNode));

            // BNF rules
            cellValue.Rule = cellValueNumber | cellValueString;
            cellReference.Rule = internalSheetCellReference | infraSheetCellReference;
            internalSheetCellReference.Rule = cellName;
            infraSheetCellReference.Rule = worksheetName + ToTerm("!") + cellName;
            expression.Rule = cellReference + cellValue;
            expressionList.Rule = MakeStarRule(expressionList, expression);
            worksheet.Rule = worksheetName + ToTerm("{") + expressionList + ToTerm("}");
            worksheetList.Rule = MakePlusRule(worksheetList, worksheet);

            // A spreadsheet is a list of worksheets...
            Root = worksheetList;

            // Punctuation  and transient terms
            RegisterBracePair("{", "}");
            MarkPunctuation("{", "}");
        }
    }
}
