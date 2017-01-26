using CommandLine;
using CommandLine.Text;

namespace Spready
{
    /// <summary>
    /// Command line options specification.
    /// </summary>
    class Options
    {
        public Options()
        {
            NewVerb = new NewSubOptions();
        }

        [VerbOption("new", HelpText = "Create a new spreadsheet.")]
        public NewSubOptions NewVerb { get; set; }

        [VerbOption("compile", HelpText = "Compile a spreadsheet.")]
        public CompileSubOptions CompileVerb { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }
}
