using System;
using System.IO;

namespace Spready
{
    class Program
    {
        private const string DefaultSpreadsheetFilename = "Spready1.spready";
        private readonly string DefaultSpreadsheetSourceCode = @"worksheet Sheet1 {" + Environment.NewLine + @"}";

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

            switch (invokedVerbInstance)
            {
                case NewSubOptions newSubOptions:
                    new Program().CreateNewSpreadsheet(newSubOptions);
                    break;

                case CompileSubOptions compileSubOptions:
                    new Program().CompileSpreadsheet(compileSubOptions);
                    break;

                default:
                    Console.Error.WriteLine("Error: unknown command.");
                    break;
            }
        }

        private void CreateNewSpreadsheet(NewSubOptions newSubOptions)
        {
            if (newSubOptions.IsDefaultOutput)
            {
                WriteDefaultSpreadsheetSources(DefaultSpreadsheetFilename);
            }
            else
            {
                foreach (var outputFilename in newSubOptions.Outputs)
                {
                    WriteDefaultSpreadsheetSources(CreateFullSourceFilenameFrom(outputFilename));
                }
            }
        }

        private string CreateFullSourceFilenameFrom(string outputFilename)
        {
            if (Path.GetExtension(outputFilename) == string.Empty)
            {
                return outputFilename + ".spready";
            }
            else
            {
                return outputFilename;
            }
        }

        private void WriteDefaultSpreadsheetSources(string filename)
        {
            File.WriteAllText(filename, DefaultSpreadsheetSourceCode);
        }

        private void CompileSpreadsheet(CompileSubOptions compileSubOptions)
        {
            foreach (var inputFilename in compileSubOptions.Inputs)
            {
                var compiler = new SpreadsheetCompiler();
                compiler.Compile(inputFilename);
            }
        }
    }
}
