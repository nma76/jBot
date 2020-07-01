using System;
using System.Threading;
using System.Threading.Tasks;
using jBot.Daemon.Configuration;
using jBot.Lib;
using jBot.Lib.Business;
using jBot.Lib.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace jBot.Daemon
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DeamonOptions _options;

        public Worker(ILogger<Worker> logger, DeamonOptions options)
        {
            _logger = logger;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("jBot deamon started at at: {time}", DateTimeOffset.Now);

            //Create a file to keep track of last used twitter id
            _logger.LogInformation("Created data storage object");
            DataStorage dataStorage = new DataStorage(_options.DataStore.DataFolder, _options.DataStore.DataFilePrefix, _options.DataStore.OverlayFolder);

            //Create authentication object
            _logger.LogInformation("Created authentication object");
            AuthToken authToken = new AuthToken()
            {
                AccessToken = _options.Authentication.AccessToken,
                AccessTokenSecret = _options.Authentication.AccessTokenSecret,
                ConsumerKey = _options.Authentication.ConsumerKey,
                ConsumerKeySecret = _options.Authentication.ConsumerKeySecret
            };

            //Create a twitter service instance
            _logger.LogInformation("Created twitter service instance");
            ServiceInstance serviceInstance = new ServiceInstance(authToken, dataStorage);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //Iterate all bots capabilities and execute them
                foreach (var capability in Capabilities.GetAll())
                {
                    ActionHandler actionHandler = new ActionHandler(serviceInstance);
                    string result = actionHandler.RunAction(capability);

                    _logger.LogInformation(result);
                }

                //Wait 30 sec and then run again
                await Task.Delay(30000, stoppingToken);
            }
        }
    }
}
