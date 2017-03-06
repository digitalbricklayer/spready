using System.Collections.Generic;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class ArgumentNodeList : AstNode
    {
        public IList<object> Arguments { get; private set; }

        public ArgumentNodeList()
        {
            Arguments = new List<object>();
        }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            foreach (var childNode in treeNode.ChildNodes)
            {
                Arguments.Add(childNode.AstNode);
            }
        }
    }
}