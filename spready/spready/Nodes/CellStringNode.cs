using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class CellStringNode : AstNode
    {
        public string Value { get; set; }

        public CellStringNode()
        {
            Value = string.Empty;
        }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            var quotedValue = treeNode.FindTokenAndGetText();
            Value = quotedValue.Trim('"');
        }
    }
}
