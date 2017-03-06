using Irony.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class SimpleStatementNode : StatementNode
    {
        public CellValueNode CellValue { get; private set; }

        public CellReferenceNode CellReference { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            CellReference = (CellReferenceNode) treeNode.ChildNodes[0].AstNode;
            CellValue = (CellValueNode) treeNode.ChildNodes[1].AstNode;
        }
    }
}
