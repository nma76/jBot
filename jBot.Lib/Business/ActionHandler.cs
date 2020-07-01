using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using jBot.Lib.Business.ImageOverlay;
using jBot.Lib.Business.SystemCommand;
using jBot.Lib.Models;
using TweetSharp;

namespace jBot.Lib.Business
{
    public class ActionHandler
    {
        private string _statusText;
        private readonly ServiceInstance _serviceInstance;
        private TwitterHelper _twitterHelper;

        public ActionHandler(ServiceInstance serviceInstance)
        {
            _statusText = "";
            _serviceInstance = serviceInstance;
            _twitterHelper = new TwitterHelper(_serviceInstance);
        }

        public string RunAction(Capability capability)
        {
            //Reset status text
            _statusText = $"Running Action Method {capability.ActionMethod}\n";

            //Call action method
            var method = typeof(ActionHandler).GetMethod(capability.ActionMethod, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            var func = (Func<Capability, string>)Delegate.CreateDelegate(typeof(Func<Capability, string>), this, method);
            return func(capability);
        }

        private string UptimeAction(Capability capability)
        {
            //filename to store sice id
            var storageIdentifier = capability.HashTag.Substring(1);

            List<string> HashTags = new List<string>() { _serviceInstance.BotConfiguration.BaseHashTag, capability.HashTag };
            List<TwitterStatus> tweets = _twitterHelper.GetTweets(storageIdentifier, HashTags);

            //Iterate all found tweets
            foreach (var tweet in tweets)
            {
                //Build the reply message based on bots capabilities
                var reply = $"@{tweet.User.ScreenName} \n\n This bot has been ";
                reply += UptimeHelper.Uptime ?? "";

                //Send tweet
                _twitterHelper.SendTweet(storageIdentifier, tweet, reply);
            }

            return _statusText;
        }

        private string HelpAction(Capability capability)
        {
            //filename to store sice id
            var storageIdentifier = capability.HashTag.Substring(1);

            //has tags to look for
            List<string> HashTags = new List<string>() { _serviceInstance.BotConfiguration.BaseHashTag, capability.HashTag };

            //Get tweets
            List<TwitterStatus> tweets = _twitterHelper.GetTweets(storageIdentifier, HashTags);

            //Iterate all found tweets
            foreach (var tweet in tweets)
            {
                try
                {
                    //Build the reply message based on bots capabilities
                    var reply = $"@{tweet.User.ScreenName} \n\n This bot have the following capabilities:\n";
                    foreach (var currentCapability in Capabilities.GetAll())
                    {
                        reply += $"{currentCapability.HashTag}: {currentCapability.Description}\n";
                    }
                    reply += $"\nAlways include {_serviceInstance.BotConfiguration.BaseHashTag} + the capability you want to execute.";

                    //Send tweet
                    _twitterHelper.SendTweet(storageIdentifier, tweet, reply);
                }

                catch { }

            }

            return _statusText;
        }

        private string FbkAction(Capability capability)
        {
            //filename to store sice id
            var storageIdentifier = capability.HashTag.Substring(1);

            List<string> HashTags = new List<string>() { _serviceInstance.BotConfiguration.BaseHashTag, capability.HashTag };
            List<TwitterStatus> tweets = _twitterHelper.GetTweets(storageIdentifier, HashTags);

            //Iterate all found tweets
            foreach (var tweet in tweets)
            {
                //Build the reply message based on bots capabilities
                var reply = $"@{tweet.User.ScreenName} \n\n Your new profile picture!\n";

                //Get authors profile image url
                var profileImageUrl = tweet.Author.ProfileImageUrl.Replace("_normal", "_400x400");

                //Get the newly created profile picture
                var overlayImageUrl = Path.Combine(_serviceInstance.BotConfiguration.DataStorage.OverleyFolder, "fbk_logo.png");

                if (File.Exists(overlayImageUrl))
                {
                    var newProfileImageUrl = OverlayHelper.CreateLogoImage(profileImageUrl, overlayImageUrl);

                    if (newProfileImageUrl != null)
                    {
                        //Send tweet
                        _twitterHelper.SendTweet(storageIdentifier, tweet, reply, newProfileImageUrl);
                    }
                }
                else
                {
                    _statusText += "Cannot find overlay image!";
                }
            }

            return _statusText;
        }
    }
}
