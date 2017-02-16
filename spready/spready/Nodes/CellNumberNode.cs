using System;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class CellNumberNode : AstNode
    {
        public int Value { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            var token = treeNode.FindTokenAndGetText();
            Value = Convert.ToInt32(token);
        }
    }
}
