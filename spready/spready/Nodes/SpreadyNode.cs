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
            WorksheetNodes = new List<WorksheetNode>();
        }

        /// <summary>
        /// Gets the inner worksheet nodes.
        /// </summary>
        public List<WorksheetNode> WorksheetNodes { get; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            foreach (var childNode in treeNode.ChildNodes)
            {
                WorksheetNodes.Add((WorksheetNode) AddChild("workspace", childNode));
            }
        }
    }
}
