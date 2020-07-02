using System;
namespace jBot.Daemon.Configuration
{
    public class DaemonOptions
    {
        public string BaseHashTag { get; set; }
        public int Interval { get; set; }
        public Authentication Authentication { get; set; }
        public DataStore DataStore { get; set; }
    }
}
