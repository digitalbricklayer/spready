using System.Collections.Generic;
using CommandLine;

namespace Spready
{
    class CompileSubOptions
    {
        public CompileSubOptions()
        {
            Inputs = new List<string>();
        }

        /// <summary>
        /// Gets or sets the spready input filenames.
        /// </summary>
        [ValueList(typeof(List<string>))]
        public List<string> Inputs { get; set; }
    }
}
