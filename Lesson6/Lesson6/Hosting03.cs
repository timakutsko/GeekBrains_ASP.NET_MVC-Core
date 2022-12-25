using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orders.DAL;
using System;
using System.Threading.Tasks;

namespace Lesson6
{
    internal class Hosting03
    {
#nullable enable
        private static IHost? _host;

        public static IHost Hosting => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(op =>
                {
                    op.AddJsonFile("appsettings.json");
                })
                .ConfigureAppConfiguration(op =>
                {
                    op
                        .AddJsonFile("appsettings.json")
                        .AddIniFile("appsettings.ini", true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureLogging(op =>
                {
                    op.ClearProviders()
                    .AddConsole()
                    .AddDebug();
                })
                .ConfigureServices(ConfigureServices);
        }

        static async Task Main(string[] args)
        {
            await Hosting.StartAsync();

            await PrintBuyersAsync();
            Console.ReadKey(true);

            await Hosting.StopAsync();
        }

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddDbContext<OrdersDbContext>(options =>
            {
                options.UseSqlServer(host.Configuration["Settings:DatabaseOptions:ConnectionString"]);
            });
        }

        private static async Task PrintBuyersAsync()
        {
            await using var serviceScope = Services.CreateAsyncScope();
            var services = serviceScope.ServiceProvider;

            OrdersDbContext context = services.GetRequiredService<OrdersDbContext>();
            ILogger logger = services.GetRequiredService<ILogger<Hosting03>>();

            foreach (var buyer in context.Buyers)
            {
                logger.LogInformation($"Покупатель >>> {buyer.LastName} {buyer.Name} {buyer.Patronymic} {buyer.Birthday.ToShortDateString()}");
            }
        }
    }
}
