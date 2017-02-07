using System;
using System.Collections.Generic;
using System.IO;
using Irony.Parsing;
using Spready.Grammar;
using Spready.Nodes;

namespace Spready.Parser
{
    internal class SpreadyParser
    {
        private readonly SpreadyGrammar grammar = new SpreadyGrammar();

        /// <summary>
        /// Parse a spready file.
        /// </summary>
        /// <param name="filename">Path to the spready source file.</param>
        /// <returns>Parse result.</returns>
        internal ParseResult<SpreadyNode> Parse(string filename)
        {
            var language = new LanguageData(grammar);
            var parser = new Irony.Parsing.Parser(language);
            if (!File.Exists(filename))
            {
                return new ParseResult<SpreadyNode>(ParseStatus.Failed,
                                                    new List<string> {"Unable to find file."});
            }
            var fileContents = File.ReadAllText(filename);
            var theParseTree = parser.Parse(fileContents);

            return CreateResultFrom(theParseTree);
        }

        private static ParseResult<SpreadyNode> CreateResultFrom(ParseTree parseTree)
        {
            switch (parseTree.Status)
            {
                case ParseTreeStatus.Error:
                    return new ParseResult<SpreadyNode>(ConvertStatusFrom(parseTree.Status),
                                                        new List<string>());

                case ParseTreeStatus.Parsed:
                    return new ParseResult<SpreadyNode>(ParseStatus.Success,
                                                        parseTree);

                default:
                    throw new NotImplementedException();
            }
        }

        private static ParseStatus ConvertStatusFrom(ParseTreeStatus status)
        {
            switch (status)
            {
                case ParseTreeStatus.Parsed:
                    return ParseStatus.Success;

                case ParseTreeStatus.Error:
                    return ParseStatus.Failed;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
