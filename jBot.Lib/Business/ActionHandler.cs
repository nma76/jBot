using System;
using System.Reflection;

namespace jBot.Lib.Business
{
    public static class ActionHandler
    {
        public static string RunAction(string methodName)
        {
            var method = typeof(ActionHandler).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
            var func = (Func<string>)Delegate.CreateDelegate(typeof(Func<string>), method);
            return func();
        }

        private static string uptimeAction()
        {
            return "Uptime Not Implemented";
        }
        private static string helpAction()
        {
            return "Help Not Implemented";
        }
        private static string fbkAction()
        {
            return "FBK Not Implemented";
        }
    }
}
