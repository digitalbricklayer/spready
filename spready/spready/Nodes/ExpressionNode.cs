using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class ExpressionNode : AstNode
    {
        public StatementNode InnerStatement { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            InnerStatement = (StatementNode) treeNode.ChildNodes[0].AstNode;
        }
    }
}
