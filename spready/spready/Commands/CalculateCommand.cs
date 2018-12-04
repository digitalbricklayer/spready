namespace Spready.Commands
{
    public class CalculateCommand : ICommand
    {
        public int Run(object options)
        {
            return (int) ExitCode.Fail;
        }
    }
}
