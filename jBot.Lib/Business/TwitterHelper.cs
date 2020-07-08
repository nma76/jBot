using System;
using System.Collections.Generic;
using System.IO;
using jBot.Lib.Models;
using System.Linq;
using TweetSharp;

namespace jBot.Lib.Business
{
    public class TwitterHelper
    {
        private ServiceInstance _serviceInstance;

        public TwitterHelper(ServiceInstance serviceInstance)
        {
            _serviceInstance = serviceInstance;
        }

        public List<TwitterStatus> SearchTweets(SearchParams searchParams, bool excludeReplies = true)
        {
            var searchOptions = new SearchOptions();

            if (searchParams.HashTags.Count > 0)
                searchOptions.Q = string.Join(' ', searchParams.HashTags);

            searchOptions.SinceId = searchParams.SinceId;

            if (searchParams.MaxItems > 0)
                searchOptions.Count = searchParams.MaxItems;

            var result = _serviceInstance.Instance.Search(searchOptions);

            if (excludeReplies)
                return result.Statuses.Where(x => x.InReplyToStatusId.Equals(null)).ToList();
            else
                return result.Statuses.ToList();
        }

        public List<TwitterStatus> GetTweets(string storageIdentifier, List<string> HashTags)
        {
            //Add search parameters to get tweets

            //Add search parameters to get tweets
            SearchParams searchParams = new SearchParams()
            {
                HashTags = HashTags,
                SinceId = _serviceInstance.BotConfiguration.DataStorage.Load(storageIdentifier)
            };

            //Search for tweets
            var tweets = SearchTweets(searchParams);

            //Add tweet count to diagnostics
            Diagnostics.AddTotalRead(tweets.Count);

            return tweets;
        }

        public void SendTweet(string storageIdentifier, TwitterStatus tweet, string reply)
        {
            //Id of tweet to reply to
            var inReplyToId = tweet.Id;

            //Send tweet. TODO: Error handling
            _ = _serviceInstance.Instance.SendTweet(new SendTweetOptions() { Status = reply, InReplyToStatusId = inReplyToId });

            //Add tweet count to diagnostics
            Diagnostics.AddTotalSent(1);

            //Update "since"-id to avoid answering to the same tweet again
            _serviceInstance.BotConfiguration.DataStorage.Save(storageIdentifier, inReplyToId);
        }

        public void SendTweet(string storageIdentifier, TwitterStatus tweet, string reply, string MediaPath)
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
                    _ = _serviceInstance.Instance.SendTweet(new SendTweetOptions() { Status = reply, InReplyToStatusId = inReplyToId, MediaIds = MediaIds });

                    //Add tweet count to diagnostics
                    Diagnostics.AddTotalSent(1);

                    //Update "since"-id to avoid answering to the same tweet again
                    _serviceInstance.BotConfiguration.DataStorage.Save(storageIdentifier, inReplyToId);
                }
            }
        }
    }
}
