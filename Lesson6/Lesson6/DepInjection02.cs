using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orders.DAL;
using System;

namespace Lesson6
{
    internal class DepInjection02
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<OrdersDbContext>(op =>
            {
                op.UseSqlServer("data source=(localdb)\\workManager;initial catalog=OrdersDatabase;User Id=Orders User;Password=12345;App=EntityFramework");
            });

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            OrdersDbContext context = serviceProvider.GetRequiredService<OrdersDbContext>();
            foreach (var buyer in context.Buyers)
            {
                Console.WriteLine($"{buyer.LastName} {buyer.Name} {buyer.Patronymic} {buyer.Birthday.ToShortDateString()}");
            }

            Console.ReadKey();
        }
    }
}
