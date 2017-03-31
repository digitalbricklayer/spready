using Spready.Parser;
using System;

namespace Spready.Commands
{
    public class CompileCommand : ICommand
    {
        public int Run(object options)
        {
            return CompileSpreadsheet((CompileSubOptions) options);
        }

        private int CompileSpreadsheet(CompileSubOptions compileSubOptions)
        {
            var inputFilename = compileSubOptions.Input;

            // Parse the source code...
            var spreadyParser = new SpreadyParser();
            var parseResult = spreadyParser.Parse(inputFilename);
            if (parseResult.Status != ParseStatus.Success)
            {
                Console.Error.WriteLine("Error: syntax error in input file.");
                return (int)ExitCode.BadSyntax;
            }

            var theRootNode = parseResult.Root;

            var compiler = new SpreadsheetCompiler();
            var compilationStatus = compiler.Compile(new SpreadsheetDetails(inputFilename, theRootNode));

            return (int)ExitCode.Success;
        }
    }
}
