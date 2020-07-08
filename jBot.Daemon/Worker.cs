using System;
using System.Threading;
using System.Threading.Tasks;
using jBot.Lib;
using jBot.Lib.Business;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace jBot.Daemon
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ServiceInstance _serviceInstance;

        public Worker(ILogger<Worker> logger, ServiceInstance serviceInstance)
        {
            _logger = logger;
            _serviceInstance = serviceInstance;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("jBot daemon started at at: {time}", DateTimeOffset.Now);
            _logger.LogInformation("Looking for hashtag {basehashtag}", _serviceInstance.BotConfiguration.BaseHashTag);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //Iterate all bots capabilities and execute them
                foreach (var capability in Capabilities.GetAll())
                {
                    _logger.LogInformation("Running capability {capability} - {description}", capability.ActionMethod, capability.Description);

                    ActionHandler actionHandler = new ActionHandler(_serviceInstance);
                    string result = actionHandler.RunAction(capability);

                    _logger.LogInformation(result);
                }

                _logger.LogInformation("Total tweet read: {readtweets}", Diagnostics.GetTotalRead());
                _logger.LogInformation("Total tweet sent: {senttweets}", Diagnostics.GetTotalSent());

                //Wait x seconds and then run again
                await Task.Delay(_serviceInstance.BotConfiguration.Interval * 1000, stoppingToken);
            }
        }
    }
}
