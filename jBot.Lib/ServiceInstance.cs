using System;
using jBot.Lib.Business;
using jBot.Lib.Models;
using TweetSharp;

namespace jBot.Lib
{
    public class ServiceInstance
    {
        public ServiceInstance(AuthToken authToken, DataStorage dataStorage)
        {
            Instance = new TwitterService(authToken.ConsumerKey, authToken.ConsumerKeySecret, authToken.AccessToken, authToken.AccessTokenSecret);
            Storage = dataStorage;
        }

        public TwitterService Instance { get; }
        public DataStorage Storage { get; set; }
    }
}
