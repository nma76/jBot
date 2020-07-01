using System;
using jBot.Lib.Business;
using jBot.Lib.Models;
using TweetSharp;

namespace jBot.Lib
{
    public class ServiceInstance
    {
        public ServiceInstance(BotConfiguration configuration)
        {
            Instance = new TwitterService(configuration.AuthToken.ConsumerKey, configuration.AuthToken.ConsumerKeySecret, configuration.AuthToken.AccessToken, configuration.AuthToken.AccessTokenSecret);
            BotConfiguration = configuration;
        }

        public TwitterService Instance { get; }
        public BotConfiguration BotConfiguration { get; set; }
    }
}
