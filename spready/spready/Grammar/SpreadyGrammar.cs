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

            var workspaceName = new StringLiteral("workspace name", "\"", StringOptions.NoEscapes);
            workspaceName.AstConfig.NodeType = typeof (WorksheetNameNode);
            var workspace = new NonTerminal("workspace", typeof(WorksheetNode));
            workspace.Rule = workspaceName + ToTerm("{") + ToTerm("}");
            var workspaceList = new NonTerminal("workspace list", typeof(SpreadyNode));
            workspaceList.Rule = MakePlusRule(workspaceList, ToTerm(","), workspace);

            Root = workspaceList;
            RegisterBracePair("{", "}");
            MarkPunctuation("{", "}");
        }
    }
}
