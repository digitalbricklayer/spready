using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class CellExpressionNode : AstNode
    {
        public string Operator { get; private set; }
        public CellReferenceBaseNode CellReference { get; private set; }
        public CellValueNode CellValue { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            CellReference = (CellReferenceBaseNode) treeNode.ChildNodes[0].AstNode;
            Operator = treeNode.ChildNodes[1].FindTokenAndGetText();
            CellValue = (CellValueNode)treeNode.ChildNodes[2].AstNode;
        }
    }
}