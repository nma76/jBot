using jBot.Daemon.Configuration;
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
                    DeamonOptions options = configuration.GetSection("Deamon").Get<DeamonOptions>();
                    services.AddSingleton(options);

                    //Add worker
                    services.AddHostedService<Worker>();
                });
    }
}
