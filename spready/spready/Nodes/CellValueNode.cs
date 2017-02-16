using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class CellValueNode : AstNode
    {
        public AstNode Value { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Value = (AstNode) treeNode.ChildNodes[0].AstNode;
        }
    }
}
