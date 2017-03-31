using System.IO;
using System.Reflection;

namespace Spready.Tests.Unit
{
    public class SpreadyFixture
    {
        protected (string inputPath, string outputPath) WriteSourcesFrom(string resourceName, string inputFilename)
        {
            var assembly = Assembly.GetExecutingAssembly();

            string sourceCode;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                sourceCode = reader.ReadToEnd();
            }

            var inputPath = Path.Combine(Path.GetTempPath(), inputFilename);
            File.WriteAllText(inputPath, sourceCode);

            var outputFilename = Path.GetFileNameWithoutExtension(inputFilename) + ".xlsx";
            var outputPath = Path.Combine(Path.GetTempPath(), outputFilename);

            return (inputPath, outputPath);
        }
    }
}