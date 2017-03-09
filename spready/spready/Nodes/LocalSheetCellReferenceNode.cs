using Irony.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class LocalSheetCellReferenceNode : XNode
    {
        public string CellName { get; private set; }

        public override string GetFullName()
        {
            return CellName;
        }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            CellName = treeNode.ChildNodes[0].FindTokenAndGetText();
        }
    }
}
