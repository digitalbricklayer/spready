using System.Collections.Generic;
using System.Linq;
using CommandLine;

namespace Spready
{
    /// <summary>
    /// New command options.
    /// </summary>
    class NewSubOptions
    {
        public NewSubOptions()
        {
            Outputs = new List<string>();
        }

        /// <summary>
        /// Gets or sets the spreadsheet filename.
        /// </summary>
        [ValueList(typeof(List<string>))]
        public List<string> Outputs { get; set; }

        /// <summary>
        /// Gets whether to create a new spreadsheet with a default name.
        /// </summary>
        public bool IsDefaultOutput => !Outputs.Any();
    }
}
