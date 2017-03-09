using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class CellReferenceNode : AstNode
    {
        public XNode CellReference { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            CellReference = (XNode) treeNode.ChildNodes[0].AstNode;
        }
    }
}
