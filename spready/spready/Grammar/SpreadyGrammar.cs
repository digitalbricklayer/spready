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
            var worksheetName = new RegexBasedTerminal("worksheet name", @"\b[A-Za-z0-9]\w*\b");
            worksheetName.AstConfig.NodeType = typeof (WorksheetNameNode);
            var cellValueNumber = new NumberLiteral("cell number", NumberOptions.None, typeof(CellNumberNode));
            var cellValueString = new StringLiteral("cell string", "\"", StringOptions.NoEscapes, typeof(CellStringNode));
            var cellName = new RegexBasedTerminal("cell name", @"((\$?[A-Za-z]{1,3})(\$?[0-9]{1,6}))"); // @"[A-Za-z]\w*[0-9]\w*", 
            cellName.AstConfig.NodeType = typeof (CellNameNode);

            // Non-termninals
            var worksheet = new NonTerminal("worksheet", typeof(WorksheetNode));
            var worksheetList = new NonTerminal("worksheet list", typeof(SpreadyNode));
            var expression = new NonTerminal("expression", typeof(ExpressionNode));
            var simpleStatement = new NonTerminal("simple statement", typeof(SimpleStatementNode));
            var equalsStatement = new NonTerminal("equals statement", typeof(EqualsStatementNode));
            var expressionList = new NonTerminal("expression list", typeof(ExpressionNodeList));
            var cellReference = new NonTerminal("cell reference", typeof(CellReferenceNode));
            var cellValue = new NonTerminal("cell value", typeof(CellValueNode));
            var localSheetCellReference = new NonTerminal("internal sheet cell reference", typeof(LocalSheetCellReferenceNode));
            var infraSheetCellReference = new NonTerminal("infra sheet cell reference", typeof(InfraSheetCellReferenceNode));
            var functionCall = new NonTerminal("function call", typeof(FunctionCallNode));
            var argumentList = new NonTerminal("argument list", typeof(FunctionCallArgumentNodeList));

            // BNF rules
            cellValue.Rule = cellValueNumber | cellValueString;
            cellReference.Rule = infraSheetCellReference | localSheetCellReference;
            localSheetCellReference.Rule = cellName;
            infraSheetCellReference.Rule = worksheetName + ToTerm("!") + cellName;
            simpleStatement.Rule = cellReference + cellValue;
            functionCall.Rule = "SUM";
            argumentList.Rule = MakeStarRule(argumentList, ToTerm(","), cellReference);
            equalsStatement.Rule = cellReference + ToTerm("=") + functionCall + ToTerm("(") + argumentList + ToTerm(")");
            expression.Rule = simpleStatement | equalsStatement;
            expressionList.Rule = MakeStarRule(expressionList, ToTerm(","), expression);
            worksheet.Rule = worksheetName + ToTerm("{") + expressionList + ToTerm("}");
            worksheetList.Rule = MakePlusRule(worksheetList, worksheet);

            // A spreadsheet is a list of worksheets...
            Root = worksheetList;

            // Punctuation  and transient terms
            RegisterBracePair("{", "}");
            RegisterBracePair("(", ")");
            MarkPunctuation("{", "}", ",", "!", "=", "(", ")");
        }
    }
}
