using Irony.Interpreter.Ast;

namespace Spready.Nodes
{
    public abstract class XNode : AstNode
    {
        public abstract string GetFullName();
    }
}