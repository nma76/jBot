using jBot.Daemon.Configuration;
using jBot.Lib;
using jBot.Lib.Business;
using jBot.Lib.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace jBot.Daemon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureServices((hostContext, services) =>
                {
                    //Add configurations
                    IConfiguration configuration = hostContext.Configuration;
                    DaemonOptions options = configuration.GetSection("Daemon").Get<DaemonOptions>();

                    //Create a file to keep track of last used twitter id
                    StorageSettings storageSettings = new StorageSettings()
                    {
                        Datafolder = options.DataStore.DataFolder,
                        FilePrefix = options.DataStore.DataFilePrefix,
                        OverlayFolder = options.DataStore.OverlayFolder
                    };
                    DataStorage dataStorage = new DataStorage(storageSettings);

                    //Create authentication object
                    AuthToken authToken = new AuthToken()
                    {
                        AccessToken = options.Authentication.AccessToken,
                        AccessTokenSecret = options.Authentication.AccessTokenSecret,
                        ConsumerKey = options.Authentication.ConsumerKey,
                        ConsumerKeySecret = options.Authentication.ConsumerKeySecret
                    };

                    //Create configuration object
                    BotConfiguration botConfiguration = new BotConfiguration
                    {
                        BaseHashTag = options.BaseHashTag,
                        AuthToken = authToken,
                        DataStorage = dataStorage,
                        Interval = options.Interval
                    };

                    //Create a twitter service instance and add it as singleton
                    ServiceInstance serviceInstance = new ServiceInstance(botConfiguration);
                    services.AddSingleton(serviceInstance);

                    //Add worker
                    services.AddHostedService<Worker>();
                });
    }
}
