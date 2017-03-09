using System.Collections.Generic;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class FunctionCallArgumentNodeList : AstNode
    {
        public IList<CellReferenceNode> Arguments { get; private set; }

        public FunctionCallArgumentNodeList()
        {
            Arguments = new List<CellReferenceNode>();
        }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            foreach (var childNode in treeNode.ChildNodes)
            {
                Arguments.Add((CellReferenceNode) childNode.AstNode);
            }
        }
    }
}