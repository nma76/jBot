using System.Linq;

namespace jBot.Lib.Business.Extension
{
    public static class Extensions
    {
        public static int ToIndexNumber(this string str, int max)
        {
            return str.ToCharArray().Sum(x => x) % max;
        }
    }
}
