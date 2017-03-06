using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class FunctionCallNode : AstNode
    {
        public string FunctionName { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            FunctionName = treeNode.ChildNodes[0].Token.Text;
        }
    }
}
