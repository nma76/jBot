using System;
using System.Threading;
using System.Threading.Tasks;

namespace jBot.Daemon
{
    public static class ConsoleHost
    {
        /// <summary>
        /// Returns a Task that completes when shutdown is triggered via the given token, Ctrl+C or SIGTERM.
        /// </summary>
        /// <param name="token">The token to trigger shutdown.</param>
        public static async Task WaitForShutdownAsync(CancellationToken token = default(CancellationToken))
        {
            var done = new ManualResetEventSlim(false);
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(token))
            {
                AttachCtrlcSigtermShutdown(cts, done, shutdownMessage: string.Empty);
                await WaitForTokenShutdownAsync(cts.Token);
                done.Set();
            }
        }

        private static void AttachCtrlcSigtermShutdown(CancellationTokenSource cts, ManualResetEventSlim resetEvent, string shutdownMessage)
        {
            Action ShutDown = () =>
            {
                if (!cts.IsCancellationRequested)
                {
                    if (!string.IsNullOrWhiteSpace(shutdownMessage))
                    {
                        Console.WriteLine(shutdownMessage);
                    }
                    try
                    {
                        cts.Cancel();
                    }
                    catch (ObjectDisposedException) { }
                }
                
                resetEvent.Wait();
            };

            AppDomain.CurrentDomain.ProcessExit += delegate { ShutDown(); };
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                ShutDown();
                
                eventArgs.Cancel = true;
            };
        }

        private static async Task WaitForTokenShutdownAsync(CancellationToken token)
        {
            var waitForStop = new TaskCompletionSource<object>();
            token.Register(obj =>
            {
                var tcs = (TaskCompletionSource<object>)obj;
                tcs.TrySetResult(null);
            }, waitForStop);
            await waitForStop.Task;
        }
    }
}
