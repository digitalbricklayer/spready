﻿using System.IO;
using NUnit.Framework;
using SpreadsheetLight;
using Spready.Commands;

namespace Spready.Tests.Unit
{
    [TestFixture]
    public class CompileCommandWithHiddenWorksheetShould
    {
        private readonly string InputFilename = "HiddenWorksheet.spready";
        private readonly string OutputFilename = "HiddenWorksheet.xlsx";
        private readonly string SourceCode = @"worksheet Summary { A1=SUM(Backing!A1, Backing!A2) } worksheet Backing hidden { 	/* Hidden worksheet. */ A1 10, A2 20 }";
        private string inputPath;
        private string outputPath;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            inputPath = Path.Combine(Path.GetTempPath(), InputFilename);
            File.WriteAllText(inputPath, SourceCode);
            outputPath = Path.Combine(Path.GetTempPath(), OutputFilename);
        }

        [OneTimeTearDown]
        public void OneTimeCleanup()
        {
            File.Delete(inputPath);
            File.Delete(outputPath);
        }

        [Test]
        public void CreateSpreadsheetWithHiddenWorksheet()
        {
            var compileCommand = new CompileCommand();
            compileCommand.Run(CreateCompileSubOptions());
            using (var spreadsheet = new SLDocument(outputPath))
            {
                var isBackingWorksheetHidden = spreadsheet.IsWorksheetHidden("Backing");
                Assert.That(isBackingWorksheetHidden, Is.True);
            }
        }

        [Test]
        public void ExitCodeReturnsSuccess()
        {
            var compileCommand = new CompileCommand();
            var exitCode = compileCommand.Run(CreateCompileSubOptions());
            Assert.That(exitCode, Is.EqualTo(0));
        }

        private CompileSubOptions CreateCompileSubOptions()
        {
            return new CompileSubOptions { Input = inputPath };
        }
    }
}
