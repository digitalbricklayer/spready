using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class WorkspaceNode : AstNode
    {
        public string Name { get { return WorkspaceName.Name; } }
        public WorkspaceNameNode WorkspaceName { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            WorkspaceName = (WorkspaceNameNode) treeNode.ChildNodes[0].AstNode;
        }
    }
}
