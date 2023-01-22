﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using MyProject.Extensions;
using MyProject.Models.Reports;
using MyProject.Services;
using MyProject.Services.Impl;
using MyProject.Services.IReports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orders.DAL;
using Orders.DAL.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject
{
    internal class Program
    {
        private static Random _random = new Random();
 
        private static WebApplication? _app;

        public static WebApplication App
        {
            //_host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
            get
            {
                if (_app == null)
                {
                    _app = CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

                    if (!_app.Environment.IsDevelopment())
                        _app.UseExceptionHandler("/Home/Error");
                    else
                        _app.UseDeveloperExceptionPage();

                    _app.UseStaticFiles();

                    _app.UseRouting();

                    _app.UseAuthorization();

                    _app.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Privacy}/{id?}");
                }
                return _app;
            }
        }

        public static IServiceProvider Services => App.Services;

        public static WebApplicationBuilder CreateHostBuilder(string[] args)
        {
            var webAppBuilder = WebApplication.CreateBuilder(args);
            
            webAppBuilder
                .Host
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(container =>
                {
                    container.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
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

            return webAppBuilder;
        }

        static async Task Main(string[] args)
        {
            await App.StartAsync();

            await PrintBuyersAsync();
            Console.ReadKey(true);

            await App.StopAsync();
        }

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddControllersWithViews();
            
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
            ILogger logger = services.GetRequiredService<ILogger<Program>>();

            foreach (var buyer in context.Buyers)
            {
                logger.LogInformation($"Покупатель >>> {buyer.LastName} {buyer.Name} {buyer.Patronymic} {buyer.Birthday.ToShortDateString()}");
            }

            var orderService = services.GetRequiredService<IOrderService>();
            Order currentOrder = await orderService.CreateAsync(_random.Next(1, 6), "Address", "+123456789", new (int, int)[] {
                    new ValueTuple<int, int>(1, 2),
                    new ValueTuple<int, int>(2, 3),
                });

            #region Чек на заказ
            OrderCatalog orderCatalog = new OrderCatalog
            {
                Name = $"Покупатель: {currentOrder.Buyer.LastName} {currentOrder.Buyer.Name} {currentOrder.Buyer.Patronymic}",
                Description = $"Товарный чек для заказа # {currentOrder.Id}",
                CreationDate = DateTime.Now,
                Items = currentOrder.Items
            };
            string checkTemplateFile = "Templates/CheckTemplate.docx";
            IReport checkReport = new OrderCheckWord(checkTemplateFile);
            //CreateReport(checkReport, orderCatalog, "CurrentCheck.docx");
            #endregion

            #region Пример с урока
            ProductsCatalog productCatalog = new ProductsCatalog
            {
                Name = "Каталог товаров",
                Description = "Актуальный список товаров на дату",
                CreationDate = DateTime.Now,
                Products = context.Products
            };
            string defaultTemplateFile = "Templates/DefaultTemplate.docx";
            IReport productReport = new ProductReportWord(defaultTemplateFile);
            //CreateReport(productReport, productCatalog, "MyReport.docx");
            #endregion

            Console.ReadKey(true);
        }

        /// <summary>
        /// Создать отчет
        /// </summary>
        /// <param name="productReport">Генератор отчета</param>
        /// <param name="catalog">Объект с данными</param>
        /// <param name="reportFileName">Наименование файла-отчета</param>
        private static void CreateReport(IReport report, ICatalog catalog, string reportFileName)
        {
            report.Name = catalog.Name;
            report.Description = catalog.Description;
            report.CreationDate = catalog.CreationDate;


            if (report is ProductReportWord)
            {
                ProductReportWord productReport = report as ProductReportWord;
                ProductsCatalog productCatalog = catalog as ProductsCatalog;
                productReport.Products = productCatalog.Products.Select(product =>
                    (
                        product.Id,
                        product.Name,
                        product.Category,
                        product.Price)
                    );
            }
            else if (report is OrderCheckWord)
            {
                OrderCheckWord orderCheckWord = report as OrderCheckWord;
                OrderCatalog orderCatalog = catalog as OrderCatalog;
                orderCheckWord.Items = orderCatalog.Items;
            }

            var reportFileInfo = report.Create(reportFileName);
            reportFileInfo.Execute();
        }
    }
}
