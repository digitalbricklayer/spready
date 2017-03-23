using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class TestGroupNode : AstNode
    {
        public string WorksheetName { get; private set; }
        public TestNodeList Tests { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            WorksheetName = treeNode.ChildNodes[1].FindTokenAndGetText();
            Tests = (TestNodeList) treeNode.ChildNodes[2].AstNode;
        }
    }
}