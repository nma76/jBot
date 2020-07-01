using System;
using System.Threading.Tasks;

namespace jBot.Daemon
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await ConsoleHost.WaitForShutdownAsync();
        }
    }
}
