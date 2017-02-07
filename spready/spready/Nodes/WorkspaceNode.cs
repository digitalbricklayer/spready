using System;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class WorkspaceNode : AstNode
    {
        public string Name { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            var workspaceNameWithQuotes = treeNode.ChildNodes[0].FindTokenAndGetText();
            Name = workspaceNameWithQuotes.Trim('"');
        }
    }
}
