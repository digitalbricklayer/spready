using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class TestNameNode : AstNode
    {
        public string Name { get; set; }

        public TestNameNode()
        {
            Name = string.Empty;
        }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            var quotedName = treeNode.FindTokenAndGetText();
            Name = quotedName.Trim('"');
        }
    }
}
