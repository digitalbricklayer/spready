using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class WorkspaceNameNode : AstNode
    {
        public string Name { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            var workspaceNameWithQuotes = treeNode.FindTokenAndGetText();
            Name = workspaceNameWithQuotes.Trim('"');
        }
    }
}
