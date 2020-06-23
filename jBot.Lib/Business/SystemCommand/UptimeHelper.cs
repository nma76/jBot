using System.Diagnostics;
using System.Runtime.InteropServices;

namespace jBot.Lib.Business.SystemCommand
{
    public static class UptimeHelper
    {
        public static string Uptime
        {
            get
            {
                string output = "";

                try
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        return "Not implemented yet";
                    }
                    else
                    {
                        Process proc = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = "uptime",
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                CreateNoWindow = true
                            }
                        };
                        proc.Start();

                        while (!proc.StandardOutput.EndOfStream)
                        {
                            output += proc.StandardOutput.ReadLine();
                        }
                        output = output.Substring(0, output.IndexOf(","));
                        output = output.Substring(output.IndexOf(" "));
                        return output;
                    }

                }
                catch { }

                return null;
            }
        }
    }
}
