using Irony.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class InfraSheetCellReferenceNode : CellReferenceBaseNode
    {
        public string CellName { get; private set; }
        public string WorksheetName { get; private set; }

        public InfraSheetCellReferenceNode()
        {
            CellName = string.Empty;
            WorksheetName = string.Empty;
        }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            WorksheetName = treeNode.ChildNodes[0].FindTokenAndGetText();
            CellName = treeNode.ChildNodes[1].FindTokenAndGetText();
        }

        public override string GetFullReference()
        {
            return WorksheetName + "!" + CellName;
        }
    }
}