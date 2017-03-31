using System;
using System.IO;
using System.Reflection;

namespace Spready.Commands
{
    /// <summary>
    /// Executes the new command.
    /// </summary>
    public class NewCommand : ICommand
    {
        private readonly string DefaultSpreadsheetSourceCode = @"worksheet Sheet1 {" + Environment.NewLine + @"}";
        private const string DefaultSpreadsheetFilename = "Spready1.spready";

        public int Run(object options)
        {
            return CreateNewSpreadsheet((NewSubOptions) options);
        }

        private int CreateNewSpreadsheet(NewSubOptions newSubOptions)
        {
            if (newSubOptions.IsDefaultOutput)
            {
                var outputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var outputPath = Path.Combine(outputFolder, DefaultSpreadsheetFilename);
                WriteDefaultSpreadsheetSources(outputPath);
            }
            else
            {
                WriteDefaultSpreadsheetSources(CreateFullSourcePathFrom(newSubOptions.Output));
            }

            return (int) ExitCode.Success;
        }

        private void WriteDefaultSpreadsheetSources(string filePath)
        {
            File.WriteAllText(filePath, DefaultSpreadsheetSourceCode);
        }

        private string CreateFullSourcePathFrom(string outputFilename)
        {
            var outputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(outputFolder, CreateFullSourceFilenameFrom(outputFilename));
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
    }
}
