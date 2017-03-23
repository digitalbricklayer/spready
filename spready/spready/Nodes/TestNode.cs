using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class TestNode : AstNode
    {
        public TestNameNode TestName { get; private set; }
        public TestExpressionNode Expression { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            TestName = (TestNameNode) treeNode.ChildNodes[1].AstNode;
            Expression = (TestExpressionNode) treeNode.ChildNodes[2].AstNode;
        }
    }
}