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
        private readonly DaemonOptions _options;

        public Worker(ILogger<Worker> logger, DaemonOptions options)
        {
            _logger = logger;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("jBot daemon started at at: {time}", DateTimeOffset.Now);

            //Create a file to keep track of last used twitter id
            DataStorage dataStorage = new DataStorage(_options.DataStore.DataFolder, _options.DataStore.DataFilePrefix, _options.DataStore.OverlayFolder);
            _logger.LogInformation("Created data storage object");

            //Create authentication object
            AuthToken authToken = new AuthToken()
            {
                AccessToken = _options.Authentication.AccessToken,
                AccessTokenSecret = _options.Authentication.AccessTokenSecret,
                ConsumerKey = _options.Authentication.ConsumerKey,
                ConsumerKeySecret = _options.Authentication.ConsumerKeySecret
            };
            _logger.LogInformation("Created authentication object");

            //Create configuration object
            BotConfiguration configuration = new BotConfiguration
            {
                BaseHashTag = _options.BaseHashTag,
                AuthToken = authToken,
                DataStorage = dataStorage
            };
            _logger.LogInformation("Created configuration object with base hashtag {hashtag}", configuration.BaseHashTag);

            //Create a twitter service instance
            ServiceInstance serviceInstance = new ServiceInstance(configuration);
            _logger.LogInformation("Created twitter service instance");
            _logger.LogInformation("Looking for hashtag {basehashtag}", _options.BaseHashTag);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //Iterate all bots capabilities and execute them
                foreach (var capability in Capabilities.GetAll())
                {
                    _logger.LogInformation("Running capability {capability} - {description}", capability.ActionMethod, capability.Description);

                    ActionHandler actionHandler = new ActionHandler(serviceInstance);
                    string result = actionHandler.RunAction(capability);

                    _logger.LogInformation(result);
                }

                //Wait 30 sec and then run again
                await Task.Delay(_options.Interval * 1000, stoppingToken);
            }
        }
    }
}
