using System;
using System.IO;
using Spready.Parser;

namespace Spready
{
    class Program
    {
        private const string DefaultSpreadsheetFilename = "Spready1.spready";
        private readonly string DefaultSpreadsheetSourceCode = @"Sheet1 {" + Environment.NewLine + @"}";

        private static void Main(string[] args)
        {
            var options = new Options();
            string invokedVerb = string.Empty;
            object invokedVerbInstance = null;
            if (!CommandLine.Parser.Default.ParseArguments(args, options, (verb, subOptions) =>
            {
                invokedVerb = verb;
                invokedVerbInstance = subOptions;
            }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            switch (invokedVerb)
            {
                case "new":
                    var newSubOptions = (NewSubOptions) invokedVerbInstance;
                    new Program().CreateNewSpreadsheet(newSubOptions);
                    break;

                case "compile":
                    var compileSubOptions = (CompileSubOptions) invokedVerbInstance;
                    new Program().CompileSpreadsheet(compileSubOptions);
                    break;
            }
        }

        private void CreateNewSpreadsheet(NewSubOptions newSubOptions)
        {
            if (newSubOptions.IsDefaultOutput)
            {
                File.WriteAllText(DefaultSpreadsheetFilename, DefaultSpreadsheetSourceCode);
            }
        }

        private void CompileSpreadsheet(CompileSubOptions compileSubOptions)
        {
            foreach (var inputFilename in compileSubOptions.Inputs)
            {
                // Parse the source code...
                var spreadyParser = new SpreadyParser();
                var parseResult = spreadyParser.Parse(inputFilename);
                if (parseResult.Status != ParseStatus.Success) return;
                var compiler = new SpreadsheetCompiler();
                compiler.Compile(parseResult.Root, inputFilename);
            }
        }
    }
}
