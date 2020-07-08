using System;
using jBot.Lib.Business;

namespace jBot.Lib.Models
{
    public class BotConfiguration
    {
        public string BaseHashTag { get; set; }
        public DataStorage DataStorage { get; set; }
        public AuthToken AuthToken { get; set; }
        public int Interval { get; set; }
    }
}
