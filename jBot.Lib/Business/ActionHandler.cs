using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using jBot.Lib.Models;
using TweetSharp;

namespace jBot.Lib.Business
{
    public static class ActionHandler
    {
        public static string RunAction(string methodName, ServiceInstance serviceInstance)
        {
            var method = typeof(ActionHandler).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
            var func = (Func<ServiceInstance, string>)Delegate.CreateDelegate(typeof(Func<ServiceInstance, string>), method);
            return func(serviceInstance);
        }

        private static string uptimeAction(ServiceInstance serviceInstance)
        {
            return "Uptime Not Implemented";
        }

        private static string helpAction(ServiceInstance serviceInstance)
        {
            //Add search parameters to get tweets
            SearchParams searchParams = new SearchParams()
            {
                HashTags = new List<string>() { "#jonikabot", "#help" },
                SinceId = serviceInstance.Storage.LastSinceID
            };

            //Search for tweets
            Search search = new Search(serviceInstance);
            var tweets = search.SearchTweets(searchParams);

            //Iterate all found tweets
            foreach (var tweet in tweets)
            {
                try
                {
                    //Build the reply message based on bots capabilities
                    var reply = $"@{tweet.User.ScreenName} \n\n This bot have the following capabilities:\n";
                    foreach(var capability in Capabilities.GetAll())
                    {
                        reply += $"{capability.HashTag}: {capability.Description}\n";
                    }
                    reply += "\nAlways include #jonikabot + the capability you want to execute.";

                    //Id of tweet to reply to
                    var inReplyToId = tweet.Id;

                    //Send tweet. TODO: Error handling
                    _ = serviceInstance.Instance.SendTweet(new SendTweetOptions() { Status = reply, InReplyToStatusId = inReplyToId });

                    //Update "since"-id to avoid answering to the same tweet again
                    serviceInstance.Storage.LastSinceID = inReplyToId;
                }

                catch { }

            }

            return "Help Not Implemented";
        }

        private static string fbkAction(ServiceInstance serviceInstance)
        {
            return "FBK Not Implemented";
        }
    }
}
