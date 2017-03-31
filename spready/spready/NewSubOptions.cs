namespace Spready
{
    /// <summary>
    /// New command options.
    /// </summary>
    public class NewSubOptions
    {
        public NewSubOptions()
        {
            Output = string.Empty;
        }

        /// <summary>
        /// Gets or sets the output filename.
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// Gets whether to create a new spreadsheet with a default name.
        /// </summary>
        public bool IsDefaultOutput => string.IsNullOrWhiteSpace(Output);
    }
}
