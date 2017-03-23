using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class WorksheetExpressionNode : AstNode
    {
        public WorksheetNameNode WorksheetName { get; private set; }
        public string Operator { get; private set; }
        public string PropertyName { get; private set; }

        public WorksheetExpressionNode()
        {
            Operator = string.Empty;
            PropertyName = string.Empty;
        }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            WorksheetName = (WorksheetNameNode) treeNode.ChildNodes[0].AstNode;
            Operator = treeNode.ChildNodes[1].FindTokenAndGetText();
            PropertyName = treeNode.ChildNodes[2].FindTokenAndGetText();
        }
    }
}