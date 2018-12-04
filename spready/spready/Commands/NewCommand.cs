using System.IO;
using System.Reflection;

namespace Spready.Commands
{
    /// <summary>
    /// Executes the new command.
    /// </summary>
    public class NewCommand : ICommand
    {
        private const string DefaultNewSpreadsheetResource = "Spready.NewTemplate.txt";
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
            File.WriteAllText(filePath, ReadDefaultSourcesFrom(DefaultNewSpreadsheetResource));
        }

        private string ReadDefaultSourcesFrom(string resourceName)
        {
            string sourceCode;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                sourceCode = reader.ReadToEnd();
            }

            return sourceCode;
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

            return outputFilename;
        }
    }
}
