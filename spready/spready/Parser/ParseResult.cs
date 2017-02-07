using System.Collections.Generic;
using System.Linq;
using Irony.Parsing;

namespace Spready.Parser
{
    internal class ParseResult<T>
    {
        internal ParseResult(ParseStatus theParseStatus, ParseTree theParseTree)
        {
            Errors = new List<string>();
            Status = theParseStatus;
            Tree = theParseTree;
            Root = (T)theParseTree.Root.AstNode;
        }

        internal ParseResult(ParseStatus theParseStatus, IEnumerable<string> theErrors)
        {
            Errors = theErrors.ToList();
            Status = theParseStatus;
        }

        /// <summary>
        /// Gets a list of errors found whilst parsing the expression.
        /// </summary>
        internal IReadOnlyCollection<string> Errors { get; private set; }

        /// <summary>
        /// Gets the parse status.
        /// </summary>
        internal ParseStatus Status { get; private set; }

        /// <summary>
        /// Gets the root AST node.
        /// </summary>
        internal T Root { get; set; }

        /// <summary>
        /// Gets the Irony parse tree.
        /// </summary>
        internal ParseTree Tree { get; set; }
    }
}
