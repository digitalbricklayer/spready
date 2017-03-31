namespace Spready.Commands
{
    /// <summary>
    /// Contract for all commands.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Run the command.
        /// </summary>
        /// <param name="options">Command options depend upon the command.</param>
        /// <returns>Process result code.</returns>
        int Run(object options);
    }
}