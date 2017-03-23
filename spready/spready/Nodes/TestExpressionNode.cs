using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class TestExpressionNode : AstNode
    {
        public TestStatementNode Statement { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Statement = (TestStatementNode)treeNode.ChildNodes[1].AstNode;
        }
    }
}