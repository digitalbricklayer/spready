using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class TestStatementNode : AstNode
    {
        public AstNode InnerExpression { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            InnerExpression = (AstNode) treeNode.ChildNodes[0].AstNode;
        }
    }
}