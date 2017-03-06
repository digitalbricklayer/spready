using System.Collections.Generic;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class ExpressionNodeList : AstNode
    {
        public ExpressionNodeList()
        {
            Expressions = new List<ExpressionNode>();
        }

        public IList<ExpressionNode> Expressions { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            foreach (var childNode in treeNode.ChildNodes)
            {
                Expressions.Add((ExpressionNode) childNode.AstNode);
            }
        }
    }
}