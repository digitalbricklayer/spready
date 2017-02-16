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
        public IList<ExpressionNode> Expressions { get; private set; }
        public ExpressionListNode ExpressionList { get; private set; }

        public WorksheetNode()
        {
            Expressions = new List<ExpressionNode>();
        }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            WorksheetName = (WorksheetNameNode) treeNode.ChildNodes[0].AstNode;
            ExpressionList = (ExpressionListNode) treeNode.ChildNodes[1].AstNode;
            foreach (var childNode in ExpressionList.ChildNodes)
            {
                Expressions.Add((ExpressionNode) childNode);
            }
        }
    }
}
