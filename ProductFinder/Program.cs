using System;
using Oakton;

namespace ProductFinder
{
    class Program
    {
        static int Main(string[] args)
        {
            return CommandExecutor.ExecuteCommand<ProgramCommand>(args);
        }
    }
}