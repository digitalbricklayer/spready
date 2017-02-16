using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class CellReferenceNode : AstNode
    {
        public AstNode CellReference { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            CellReference = (AstNode) treeNode.ChildNodes[0].AstNode;
        }
    }
}
