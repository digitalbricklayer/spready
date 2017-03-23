using System.Collections.Generic;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class TestNodeList : AstNode
    {
        public IList<TestNode> Tests { get; private set; }

        public TestNodeList()
        {
            Tests = new List<TestNode>();
        }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            foreach (var testNode in treeNode.ChildNodes)
            {
                Tests.Add((TestNode) testNode.AstNode);
            }
        }
    }
}