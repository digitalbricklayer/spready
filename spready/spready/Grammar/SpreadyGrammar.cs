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

            var WORKSHEET_DECL = ToTerm("worksheet");

            // Terminals
            var worksheetName = new RegexBasedTerminal("worksheet name", @"\b[A-Za-z0-9]\w*\b");
            worksheetName.AstConfig.NodeType = typeof (WorksheetNameNode);
            var cellValueNumber = new NumberLiteral("cell number", NumberOptions.None, typeof(CellNumberNode));
            var cellValueString = new StringLiteral("cell string", "\"", StringOptions.NoEscapes, typeof(CellStringNode));
            var cellName = new RegexBasedTerminal("cell name", @"((\$?[A-Za-z]{1,3})(\$?[0-9]{1,6}))"); 
            cellName.AstConfig.NodeType = typeof (CellNameNode);
            var testName = new StringLiteral("test name", "\"", StringOptions.NoEscapes, typeof(TestNameNode));

            // Non-termninals
            var lineComment = new CommentTerminal("line comment", "//", "\n", "\r\n");
            var blockComment = new CommentTerminal("block comment", "/*", "*/");
            NonGrammarTerminals.Add(lineComment);
            NonGrammarTerminals.Add(blockComment);
            var worksheet = new NonTerminal("worksheet", typeof(WorksheetNode));
            var rootList = new NonTerminal("worksheet list", typeof(SpreadyNode));
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
            var optionalHiddenAttribute = new NonTerminal("hidden attribtue", typeof(HiddenAttributeNode));
            var rootItem = new NonTerminal("root item", typeof(RootItemNode));
            var testGroup = new NonTerminal("test group", typeof(TestGroupNode));
            var testList = new NonTerminal("test list", typeof(TestNodeList));
            var test = new NonTerminal("test", typeof(TestNode));
            var testExpression = new NonTerminal("test expression", typeof(TestExpressionNode));
            var comparisonOperator = new NonTerminal("comparison operator", typeof(ComparisonOperatorNode));
            var worksheetExpression = new NonTerminal("worksheet expression", typeof(WorksheetExpressionNode));
            var cellExpression = new NonTerminal("cell expression", typeof(CellExpressionNode));
            var testStatement = new NonTerminal("test statement", typeof(TestStatementNode));

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
            optionalHiddenAttribute.Rule = "hidden" | Empty;
            worksheet.Rule = WORKSHEET_DECL + worksheetName + optionalHiddenAttribute + ToTerm("{") + expressionList + ToTerm("}");
            rootList.Rule = MakePlusRule(rootList, worksheet);
            comparisonOperator.Rule = ToTerm("=") | ToTerm("<>");
            rootItem.Rule = worksheet | testGroup;
            rootList.Rule = MakePlusRule(rootList, rootItem);
            worksheetExpression.Rule = worksheetName + "is" + "hidden";
            cellExpression.Rule = localSheetCellReference + comparisonOperator + cellValue;
            testStatement.Rule = cellExpression | worksheetExpression;
            testExpression.Rule = ToTerm("=>") + testStatement;
            test.Rule = "test" + testName + testExpression;
            testList.Rule = MakeStarRule(testList, ToTerm(","), test);
            testGroup.Rule = "group" + WORKSHEET_DECL + worksheetName + ToTerm("{") + testList + ToTerm("}");

            // A spreadsheet is a list of worksheet declarations and test groups...
            Root = rootList;

            // Punctuation  and transient terms
            RegisterBracePair("{", "}");
            RegisterBracePair("(", ")");
            MarkPunctuation("{", "}", ",", "!", "=", "(", ")");
            MarkPunctuation(WORKSHEET_DECL);
        }
    }
}
