using System;
using jBot.Lib.Models;
using TweetSharp;

namespace jBot.Lib
{
    public class ServiceInstance
    {
        public ServiceInstance(AuthToken authToken)
        {
            Instance = new TwitterService(authToken.ConsumerKey, authToken.ConsumerKeySecret, authToken.AccessToken, authToken.AccessTokenSecret);
        }

        public TwitterService Instance { get; }
    }
}
