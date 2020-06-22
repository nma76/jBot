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
            _statusText = $"Running Action Method {methodName}\n";
            
            //Call action method
            var method = typeof(ActionHandler).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            var func = (Func<ServiceInstance, string>)Delegate.CreateDelegate(typeof(Func<ServiceInstance, string>), method);
            return func(serviceInstance);
        }

        private static string UptimeAction(ServiceInstance serviceInstance)
        {
            //filename to store sice id
            var storageIdentifier = "uptime";

            List<string> HashTags = new List<string>() { "#jonikabot", "#uptime" };
            List<TwitterStatus> tweets = GetTweets(serviceInstance, storageIdentifier, HashTags);

            //Iterate all found tweets
            foreach (var tweet in tweets)
            {
                //Build the reply message based on bots capabilities
                var reply = $"@{tweet.User.ScreenName} \n\n xx xxx xxxxx xx xxx xx\n";

                //Send tweet
                SendTweet(serviceInstance, storageIdentifier, tweet, reply);
            }

            return _statusText;
        }

        private static string HelpAction(ServiceInstance serviceInstance)
        {
            //filename to store sice id
            var storageIdentifier = "help";

            //has tags to look for
            List<string> HashTags = new List<string>() { "#jonikabot", "#help" };

            //Get tweets
            List<TwitterStatus> tweets = GetTweets(serviceInstance, storageIdentifier, HashTags);
	
            //Iterate all found tweets
            foreach (var tweet in tweets)
            {
                try
                {
                    //Build the reply message based on bots capabilities
                    var reply = $"@{tweet.User.ScreenName} \n\n This bot have the following capabilities:\n";
                    foreach (var capability in Capabilities.GetAll())
                    {
                        reply += $"{capability.HashTag}: {capability.Description}\n";
                    }
                    reply += "\nAlways include #jonikabot + the capability you want to execute.";

                    //Send tweet
                    SendTweet(serviceInstance, storageIdentifier, tweet, reply);
                }

                catch { }

            }

            return _statusText;
        }

        private static string FbkAction(ServiceInstance serviceInstance)
        {
            //filename to store sice id
            var storageIdentifier = "fbk";

            List<string> HashTags = new List<string>() { "#jonikabot", "#fbk" };
            List<TwitterStatus> tweets = GetTweets(serviceInstance, storageIdentifier, HashTags);

            //Iterate all found tweets
            foreach (var tweet in tweets)
            {
                //Build the reply message based on bots capabilities
                var reply = $"@{tweet.User.ScreenName} \n\n xx xxx xxxxx xx xxx xx\n";

                //Send tweet
                SendTweet(serviceInstance, storageIdentifier, tweet, reply);
            }

            return _statusText;
        }

        private static List<TwitterStatus> GetTweets(ServiceInstance serviceInstance, string storageIdentifier, List<string> HashTags)
        {
            //Add search parameters to get tweets
            _statusText += $"Looking for tweets with hash tags {string.Join(" ", HashTags)}\n";

            //Add search parameters to get tweets
            SearchParams searchParams = new SearchParams()
            {
                HashTags = HashTags,
                SinceId = serviceInstance.Storage.Load(storageIdentifier)
            };

            //Search for tweets
            Search search = new Search(serviceInstance);
            var tweets = search.SearchTweets(searchParams);
            _statusText += $"Found {tweets.Count} tweets\n";
            return tweets;
        }

        private static void SendTweet(ServiceInstance serviceInstance, string storageIdentifier, TwitterStatus tweet, string reply)
        {
            //Id of tweet to reply to
            var inReplyToId = tweet.Id;

            //Send tweet. TODO: Error handling
            _statusText += $"Replying to {tweet.User.ScreenName}\n";
            _ = serviceInstance.Instance.SendTweet(new SendTweetOptions() { Status = reply, InReplyToStatusId = inReplyToId });

            //Update "since"-id to avoid answering to the same tweet again
            serviceInstance.Storage.Save(storageIdentifier, inReplyToId);
        }
    }
}
