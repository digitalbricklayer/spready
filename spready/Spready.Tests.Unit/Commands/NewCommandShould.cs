using System.IO;
using System.Reflection;
using NUnit.Framework;
using Spready.Commands;

namespace Spready.Tests.Unit.Commands
{
    [TestFixture]
    public class NewCommandShould
    {
        private readonly string DefaultSourceFilename = "Spready1.spready";
        private string outputPath;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            outputPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                      DefaultSourceFilename);
        }

        [OneTimeTearDown]
        public void OneTimeCleanup()
        {
            File.Delete(outputPath);
        }

        [Test]
        public void CreateDefaultSourceFile()
        {
            var compileCommand = new NewCommand();
            compileCommand.Run(CreateNewSubOptions());
            Assert.True(File.Exists(outputPath));
        }

        private NewSubOptions CreateNewSubOptions()
        {
            return new NewSubOptions();
        }
    }
}
