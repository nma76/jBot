using System;
using jBot.Lib;
using jBot.Lib.Business;
using jBot.Lib.Models;
using Microsoft.Extensions.Configuration;

namespace jBot.TweetConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Configure configuration file
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            //Read configurarion sections
            var authSection = Configuration.GetSection("Authentication");
            var dataFileSection = Configuration.GetSection("DataStore");

            //Create a file to keep track of last used twitter id
            DataStorage dataStorage = new DataStorage(dataFileSection["DataFolder"], dataFileSection["DataFilePrefix"], dataFileSection["OverlayFolder"]);

            //Create authentication object
            AuthToken authToken = new AuthToken()
            {
                AccessToken = authSection["AccessToken"],
                AccessTokenSecret = authSection["AccessTokenSecret"],
                ConsumerKey = authSection["ConsumerKey"],
                ConsumerKeySecret = authSection["ConsumerKeySecret"]
            };

            //Create configuration object
            BotConfiguration configuration = new BotConfiguration
            {
                BaseHashTag = "#jonikabot",
                AuthToken = authToken,
                DataStorage = dataStorage
            };

            //Create a twitter service instance
            ServiceInstance serviceInstance = new ServiceInstance(configuration);

            //Create action handler and make sure it gets called
            foreach (var capability in Capabilities.GetAll())
            {
                ActionHandler actionHandler = new ActionHandler(serviceInstance);
                string result = actionHandler.RunAction(capability);

                Console.WriteLine(result);
            }
        }
    }
}
