using System.Collections.Generic;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class WorksheetNode : AstNode
    {
        public string Name { get { return WorksheetName.Name; } }
        public WorksheetNameNode WorksheetName { get; private set; }
        public IList<StatementNode> Statements { get; private set; }
        public ExpressionNodeList ExpressionList { get; private set; }

        public WorksheetNode()
        {
            Statements = new List<StatementNode>();
        }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            WorksheetName = (WorksheetNameNode) treeNode.ChildNodes[0].AstNode;
            ExpressionList = (ExpressionNodeList) treeNode.ChildNodes[1].AstNode;
            foreach (var childNode in ExpressionList.Expressions)
            {
                Statements.Add(childNode.InnerStatement);
            }
        }
    }
}
