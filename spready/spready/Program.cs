using Spready.Commands;
using System;

namespace Spready
{
    class Program
    {
        private static int Main(string[] args)
        {
            var options = new Options();
            object invokedVerbInstance = null;
            if (!CommandLine.Parser.Default.ParseArguments(args, options, (verb, subOptions) =>
            {
                invokedVerbInstance = subOptions;
            }))
            {
                return (int)ExitCode.BadArguments;
            }

            var command = CreateCommandFrom(invokedVerbInstance);
            return command.Run(invokedVerbInstance);
        }

        private static ICommand CreateCommandFrom(object invokedVerbInstance)
        {
            switch (invokedVerbInstance)
            {
                case NewSubOptions _:
                    return new NewCommand();

                case CompileSubOptions _:
                    return new CompileCommand();

                case CalculateSubOptions _:
                    return new CalculateCommand();

                default:
                    throw new NotImplementedException("Error: unknown command.");
            }
        }
    }
}
