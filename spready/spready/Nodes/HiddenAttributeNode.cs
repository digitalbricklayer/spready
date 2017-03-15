using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;
using System.Linq;

namespace Spready.Nodes
{
    public class HiddenAttributeNode : AstNode
    {
        public bool IsVisible { get; private set; }

        public HiddenAttributeNode()
        {
            IsVisible = true;
        }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            if (treeNode.ChildNodes.Any())
                IsVisible = false;
        }
    }
}
