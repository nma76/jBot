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

        public ActionHandler(ServiceInstance serviceInstance)
        {
            _statusText = "";
            _serviceInstance = serviceInstance;
        }

        public string RunAction(string methodName)
        {
            //Reset status text
            _statusText = $"Running Action Method {methodName}\n";

            //Call action method
            var method = typeof(ActionHandler).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            var func = (Func<string>)Delegate.CreateDelegate(typeof(Func<string>), this, method);
            return func();
        }

        private string UptimeAction()
        {
            //filename to store sice id
            var storageIdentifier = "uptime";

            List<string> HashTags = new List<string>() { "#jonikabot", "#uptime" };
            List<TwitterStatus> tweets = GetTweets(storageIdentifier, HashTags);

            //Iterate all found tweets
            foreach (var tweet in tweets)
            {
                //Build the reply message based on bots capabilities
                var reply = $"@{tweet.User.ScreenName} \n\n This bot has been ";
                reply += UptimeHelper.Uptime ?? "";

                //Send tweet
                SendTweet(storageIdentifier, tweet, reply);
            }

            return _statusText;
        }

        private string HelpAction()
        {
            //filename to store sice id
            var storageIdentifier = "help";

            //has tags to look for
            List<string> HashTags = new List<string>() { "#jonikabot", "#help" };

            //Get tweets
            List<TwitterStatus> tweets = GetTweets(storageIdentifier, HashTags);

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
                    SendTweet(storageIdentifier, tweet, reply);
                }

                catch { }

            }

            return _statusText;
        }

        private string FbkAction()
        {
            //filename to store sice id
            var storageIdentifier = "fbk";

            List<string> HashTags = new List<string>() { "#jonikabot", "#fbk" };
            List<TwitterStatus> tweets = GetTweets(storageIdentifier, HashTags);

            //Iterate all found tweets
            foreach (var tweet in tweets)
            {
                //Build the reply message based on bots capabilities
                var reply = $"@{tweet.User.ScreenName} \n\n Your new profile picture!\n";

                //Get authors profile image url
                var profileImageUrl = tweet.Author.ProfileImageUrl.Replace("_normal", "_400x400");

                //Get the newly created profile picture
                var overlayImageUrl = Path.Combine(_serviceInstance.Storage.OverleyFolder, "fbk_logo.png");

                if (File.Exists(overlayImageUrl))
                {
                    var newProfileImageUrl = OverlayHelper.CreateLogoImage(profileImageUrl, overlayImageUrl);

                    if (newProfileImageUrl != null)
                    {
                        //Send tweet
                        SendTweet(storageIdentifier, tweet, reply, newProfileImageUrl);
                    }
                }
                else
                {
                    _statusText += "Cannot find overlay image!";
                }
            }

            return _statusText;
        }

        private List<TwitterStatus> GetTweets(string storageIdentifier, List<string> HashTags)
        {
            //Add search parameters to get tweets
            _statusText += $"Looking for tweets with hash tags {string.Join(" ", HashTags)}\n";

            //Add search parameters to get tweets
            SearchParams searchParams = new SearchParams()
            {
                HashTags = HashTags,
                SinceId = _serviceInstance.Storage.Load(storageIdentifier)
            };

            //Search for tweets
            Search search = new Search(_serviceInstance);
            var tweets = search.SearchTweets(searchParams);
            _statusText += $"Found {tweets.Count} tweets\n";
            return tweets;
        }

        private void SendTweet(string storageIdentifier, TwitterStatus tweet, string reply)
        {
            //Id of tweet to reply to
            var inReplyToId = tweet.Id;

            //Send tweet. TODO: Error handling
            _statusText += $"Replying to {tweet.User.ScreenName}\n";
            _ = _serviceInstance.Instance.SendTweet(new SendTweetOptions() { Status = reply, InReplyToStatusId = inReplyToId });

            //Update "since"-id to avoid answering to the same tweet again
            _serviceInstance.Storage.Save(storageIdentifier, inReplyToId);
        }

        private void SendTweet(string storageIdentifier, TwitterStatus tweet, string reply, string MediaPath)
        {
            //Id of tweet to reply to
            var inReplyToId = tweet.Id;

            if (File.Exists(MediaPath))
            {
                using (var stream = new FileStream(MediaPath, FileMode.Open))
                {
                    var Media = _serviceInstance.Instance.UploadMedia(new UploadMediaOptions() { Media = new MediaFile() { FileName = MediaPath, Content = stream } });
                    List<string> MediaIds = new List<string>
                    {
                        Media.Media_Id
                    };

                    //Send tweet. TODO: Error handling
                    _statusText += $"Replying to {tweet.User.ScreenName}\n";
                    _ = _serviceInstance.Instance.SendTweet(new SendTweetOptions() { Status = reply, InReplyToStatusId = inReplyToId, MediaIds = MediaIds });

                    //Update "since"-id to avoid answering to the same tweet again
                    _serviceInstance.Storage.Save(storageIdentifier, inReplyToId);
                }
            }
        }
    }
}
