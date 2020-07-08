using System;
namespace jBot.Lib.Business
{
    public static class Diagnostics
    {
        private static int _totalReadTweets;
        private static int _totalSentTweets;

        public static void AddTotalRead(int amount) => _totalReadTweets += amount;

        public static int GetTotalRead() => _totalReadTweets;

        public static void AddTotalSent(int amount) => _totalSentTweets += amount;

        public static int GetTotalSent() => _totalSentTweets;
    }
}
