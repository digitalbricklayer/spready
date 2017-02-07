using System.Collections.Generic;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Spready.Nodes
{
    public class SpreadyNode : AstNode
    {
        public SpreadyNode()
        {
            WorkspaceNodes = new List<WorkspaceNode>();
        }

        /// <summary>
        /// Gets the inner workspace nodes.
        /// </summary>
        public List<WorkspaceNode> WorkspaceNodes { get; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            foreach (var childNode in treeNode.ChildNodes)
            {
                WorkspaceNodes.Add((WorkspaceNode) AddChild("workspace", childNode));
            }
        }
    }
}
