using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Lesson6.Autofac;
using Lesson6.Services;
using Lesson6.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orders.DAL;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Lesson6
{
    internal class AutofacDepInjection04
    {
#nullable enable
        private static IHost? _host;

        private static Random _random = new Random();

        public static IHost Hosting => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(container =>
                {
                    // Варианты регистрации сервисов
                    container.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
                    //container.RegisterType<OrderService>().InstancePerLifetimeScope();
                    //container.RegisterModule<ServicesModule>();
                    //container.RegisterAssemblyModules(Assembly.GetCallingAssembly());

                    //var config = new ConfigurationBuilder().AddJsonFile("autofac.config.json", false, false);
                    //var module = new ConfigurationModule(config.Build());
                    //var builder = new ContainerBuilder();
                    //builder.RegisterModule(module);
                })
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
            
            var orderService = services.GetRequiredService<IOrderService>();
            await orderService.CreateAsync(_random.Next(1, 6), "Address", "+123456789", new (int, int)[] {
                    new ValueTuple<int, int>(1, 1)
                });
        }
    }
}
