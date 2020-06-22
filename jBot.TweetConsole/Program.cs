using System;
using Microsoft.Extensions.Configuration;
using jBot.Lib.Models;
using System.Collections.Generic;
using jBot.Lib;
using jBot.Lib.Business;

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
            var dataFileSection = Configuration.GetSection("DataFile");

            //Create a file to keep track of last used twitter id
            DataStorage dataStorage = new DataStorage(dataFileSection["Folder"], dataFileSection["FilePrefix"]);

            //Create authentication object
            AuthToken authToken = new AuthToken()
            {
                AccessToken = authSection["AccessToken"],
                AccessTokenSecret = authSection["AccessTokenSecret"],
                ConsumerKey = authSection["ConsumerKey"],
                ConsumerKeySecret = authSection["ConsumerKeySecret"]
            };

            //Create a twitter service instance
            ServiceInstance serviceInstance = new ServiceInstance(authToken, dataStorage);

            //Create action handler and make sure it gets called
            foreach (var capability in Capabilities.GetAll())
            {                
                Console.WriteLine(ActionHandler.RunAction(capability.ActionMethod, serviceInstance));
            }
        }
    }
}
