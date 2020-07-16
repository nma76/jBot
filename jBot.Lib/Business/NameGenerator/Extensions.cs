using System.Linq;

namespace jBot.Referee.Business
{
    public static class Extensions
    {
        public static int ToNumber(this string str, int max)
        {
            return str.ToCharArray().Sum(x => x) % max;
        }
    }
}
