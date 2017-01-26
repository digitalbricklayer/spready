using Irony.Parsing;

namespace Spready.Grammar
{
    [Language("Spready Language", "0.1", "A grammar for the Spready language.")]
    internal class SpreadyGrammar : Irony.Parsing.Grammar
    {
        public SpreadyGrammar()
            : base(caseSensitive: false)
        {
            LanguageFlags = LanguageFlags.CreateAst |
                            LanguageFlags.NewLineBeforeEOF;


        }
    }
}
