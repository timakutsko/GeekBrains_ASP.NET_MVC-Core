using Microsoft.EntityFrameworkCore;
using Orders.DAL;
using Orders.DAL.Entities;
using System;
using System.Linq;

namespace Lesson6
{
    internal class EFCore01
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder<OrdersDbContext>()
                .UseSqlServer("data source=(localdb)\\workManager;initial catalog=OrdersDatabase;User Id=Orders User;Password=12345;App=EntityFramework");

            using (OrdersDbContext context = new OrdersDbContext(dbContextOptionsBuilder.Options))
            {
                if (context.Database.EnsureCreated())
                {
                    if (!context.Buyers.Any())
                    {
                        context.Buyers.Add(new Buyer
                        {
                            LastName = "Трофимов",
                            Name = "Алексей",
                            Patronymic = "Артёмович",
                            Birthday = DateTime.Now.AddYears(-23).Date,
                        });
                        context.Buyers.Add(new Buyer
                        {
                            LastName = "Зеленин",
                            Name = "Николай",
                            Patronymic = "Даниилович",
                            Birthday = DateTime.Now.AddYears(-36).Date,
                        });
                        context.Buyers.Add(new Buyer
                        {
                            LastName = "Ермаков",
                            Name = "Фёдор",
                            Patronymic = "Дмитриевич",
                            Birthday = DateTime.Now.AddYears(-19).Date,
                        });
                        context.Buyers.Add(new Buyer
                        {
                            LastName = "Смирнова",
                            Name = "Ангелина",
                            Patronymic = "Данииловна",
                            Birthday = DateTime.Now.AddYears(-31).Date,
                        });
                        context.Buyers.Add(new Buyer
                        {
                            LastName = "Белоусова",
                            Name = "Мария",
                            Patronymic = "Денисовна",
                            Birthday = DateTime.Now.AddYears(-26).Date,
                        });
                    }

                    context.SaveChanges();

                    foreach (var buyer in context.Buyers)
                    {
                        Console.WriteLine($"{buyer.LastName} {buyer.Name} {buyer.Patronymic} {buyer.Birthday.ToShortDateString()}");
                    }
                }
                else
                    Console.WriteLine($"ERROR with DB");

            }

            Console.ReadKey();
        }
    }
}
