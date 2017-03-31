using Spready.Commands;
using System;

namespace Spready
{
    class Program
    {
        private static int Main(string[] args)
        {
            var options = new Options();
            string invokedVerb = string.Empty;
            object invokedVerbInstance = null;
            if (!CommandLine.Parser.Default.ParseArguments(args, options, (verb, subOptions) =>
            {
                invokedVerb = verb;
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
                case NewSubOptions newSubOptions:
                    return new NewCommand();

                case CompileSubOptions compileSubOptions:
                    return new CompileCommand();

                default:
                    throw new NotImplementedException("Error: unknown command.");
            }
        }
    }
}
