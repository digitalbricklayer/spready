using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace Spready.Tests.Unit
{
    public abstract class SpreadyFixture
    {
        private const string SourceNamespace = "Spready.Tests.Unit.Sources";

        protected abstract string InputFilename { get; }
        protected string InputPath { get; private set; }
        protected string OutputPath { get; private set; }

        public SpreadyFixture()
        {
            InputPath = string.Empty;
            OutputPath = string.Empty;
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            (InputPath, OutputPath) = WriteSourcesFrom(InputFilename);
        }

        [OneTimeTearDown]
        public void OneTimeCleanup()
        {
            File.Delete(InputPath);
            File.Delete(OutputPath);
        }

        protected (string inputPath, string outputPath) WriteSourcesFrom(string inputFilename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = SourceNamespace + "." + inputFilename;
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