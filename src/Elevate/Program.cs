using System;
using System.Diagnostics;
using System.Linq;

namespace Elevate
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("USAGE: elevate command [args...]");
                return 1;
            }

            var command = args[0];
            var arguments = string.Join(" ", args.Skip(1));

            if (command.ToLower() == "start")
            {
                command = "cmd";
                arguments = "/C \"cd /D " + Environment.CurrentDirectory +" && start " + arguments + "\"";
            }

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    UseShellExecute = true,
                    Verb = "runas"
                }
            };

            try
            {
                process.Start();

                process.WaitForExit();
            }
            catch
            {
                return 1;
            }

            return process.ExitCode;
        }
    }
}
