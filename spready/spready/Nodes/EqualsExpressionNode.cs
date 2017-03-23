using Irony.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class EqualsStatementNode : StatementNode
    {
        public FunctionCallArgumentNodeList FunctionCallArguments { get; set; }

        public object FunctionCall { get; private set; }

        public object AssignTo { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            AssignTo = treeNode.ChildNodes[0].AstNode;
            FunctionCall = treeNode.ChildNodes[2].AstNode;
            FunctionCallArguments = (FunctionCallArgumentNodeList) treeNode.ChildNodes[3].AstNode;
        }
    }
}