using System.Collections.Generic;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;
using System;

namespace Spready.Nodes
{
    public class SpreadyNode : AstNode
    {
        public SpreadyNode()
        {
            WorksheetNodes = new List<WorksheetNode>();
            TestGroupNodes = new List<TestGroupNode>();
        }

        /// <summary>
        /// Gets the worksheet nodes.
        /// </summary>
        public List<WorksheetNode> WorksheetNodes { get; }

        /// <summary>
        /// Gets the test group nodes.
        /// </summary>
        public List<TestGroupNode> TestGroupNodes { get; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            foreach (var childNode in treeNode.ChildNodes)
            {
                var x = AddChild("workspace", childNode);
                switch (x)
                {
                    case WorksheetNode worksheetNode:
                        WorksheetNodes.Add(worksheetNode);
                        break;

                    case TestGroupNode testGroupNode:
                        TestGroupNodes.Add(testGroupNode);
                        break;

                    default:
                        throw new NotImplementedException("Unknown spready child node.");
                }
            }
        }
    }
}
