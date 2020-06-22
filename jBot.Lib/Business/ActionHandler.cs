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
        private static string _statusText;

        public static string RunAction(string methodName, ServiceInstance serviceInstance)
        {
            //Reset status text
            _statusText = $"Running Action Method {methodName}";
            
            //Call action method
            var method = typeof(ActionHandler).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            var func = (Func<ServiceInstance, string>)Delegate.CreateDelegate(typeof(Func<ServiceInstance, string>), method);
            return func(serviceInstance);
        }

        private static string UptimeAction(ServiceInstance serviceInstance)
        {
            return "Uptime Not Implemented";
        }

        private static string HelpAction(ServiceInstance serviceInstance)
        {
            //filename to store sice id
            var storageIdentifier = "jonikabot_help";

            //Add search parameters to get tweets
            _statusText += "Looking for tweets with hash tags #jonikabot and #help\n";
            
            //Add search parameters to get tweets
            SearchParams searchParams = new SearchParams()
            {
                HashTags = new List<string>() { "#jonikabot", "#help" },
                SinceId = serviceInstance.Storage.Load(storageIdentifier)
            };

            //Search for tweets
            Search search = new Search(serviceInstance);
            var tweets = search.SearchTweets(searchParams);
	        _statusText += $"Found {tweets.Count} tweets\n";
	
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
                    _statusText += $"Replying to {tweet.User.ScreenName}\n";
                    _ = serviceInstance.Instance.SendTweet(new SendTweetOptions() { Status = reply, InReplyToStatusId = inReplyToId });

                    //Update "since"-id to avoid answering to the same tweet again
                    serviceInstance.Storage.Save(storageIdentifier, inReplyToId);
                }

                catch { }

            }

            return _statusText;
        }

        private static string FbkAction(ServiceInstance serviceInstance)
        {
            return "FBK Not Implemented";
        }
    }
}
