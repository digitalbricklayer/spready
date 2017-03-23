using Irony.Interpreter.Ast;

namespace Spready.Nodes
{
    public abstract class CellReferenceBaseNode : AstNode
    {
        /// <summary>
        /// Get the full reference.
        /// </summary>
        /// <returns>Full reference for the cell.</returns>
        public abstract string GetFullReference();
    }
}