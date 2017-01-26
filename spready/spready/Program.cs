using System;
using System.IO;

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
                // if parsing succeeds the verb name and correct instance
                // will be passed to onVerbCommand delegate (string,object)
                invokedVerb = verb;
                invokedVerbInstance = subOptions;
            }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            switch (invokedVerb)
            {
                case "new":
                    {
                        var newSubOptions = (NewSubOptions) invokedVerbInstance;
                        new Program().CreateNewSpreadsheet(newSubOptions);
                    }
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

        private void CompileSpreadsheet(CompileSubOptions newSubOptions)
        {
            throw new NotImplementedException();
        }
    }
}
