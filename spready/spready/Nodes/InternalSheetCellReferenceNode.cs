using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class InternalSheetCellReferenceNode : AstNode
    {
        public string CellName { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            CellName = treeNode.ChildNodes[0].FindTokenAndGetText();
        }
    }
}
