using Lesson4.StoreTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Lesson4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<object> allProducts = new List<object>();

            DirectoryInfo dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

            FileInfo streetTradingInfo = dirInfo.GetFiles().Where(x => x.Name.Equals("StreetTrading.json")).FirstOrDefault();
            FileInfo supermarketInfo = dirInfo.GetFiles().Where(x => x.Name.Equals("Supermarket.json")).FirstOrDefault();

            string jsonStreetTrading = File.ReadAllText(streetTradingInfo.FullName);
            string jsonSupermarket = File.ReadAllText(supermarketInfo.FullName);

            StreetTrading dataStreetTrading = JsonSerializer.Deserialize<StreetTrading>(jsonStreetTrading);
            Supermarket dataSupermarket = JsonSerializer.Deserialize<Supermarket>(jsonSupermarket);

            allProducts.AddRange(dataStreetTrading.ProductFruits);
            allProducts.AddRange(dataStreetTrading.ProductVegetables);
            allProducts.AddRange(dataSupermarket.Fruits);
            allProducts.AddRange(dataSupermarket.Vegetables);
            allProducts.AddRange(dataSupermarket.Toys);

            ProductPrinter.PrintTitle();
            foreach (var item in allProducts)
            {
                ProductAdapter productAdapter;
                StreetTradingEntity castToStreetTradingEntity = item as StreetTradingEntity;
                SupermarketEntity castToSupermarketEntity = item as SupermarketEntity;
                
                if (castToStreetTradingEntity != null)
                {
                    productAdapter = new StreetTradingEntityAdapter(castToStreetTradingEntity);
                }
                else if (castToSupermarketEntity != null)
                {
                    productAdapter = new SupermarketEntityAdapter(castToSupermarketEntity);
                }
                else
                {
                    throw new InvalidCastException("Новый тип продукта!");
                }

                ProductPrinter.Print(productAdapter);
            }
        }

        internal static class ProductPrinter
        {
            public static void PrintTitle()
            {
                Console.WriteLine("Спецификация всех магазинов:");
                Console.WriteLine("Имя\t\tЦена\t\tСкидка\t\tИтоговая стоимость");
            }

            public static void Print(ProductAdapter product)
            {
                Console.WriteLine($"{product.GetItemName()}" +
                    $"\t\t{product.GetItemPrice()}" +
                    $"\t\t{product.GetItemDiscount()}" +
                    $"\t\t{product.GetTotalPrice()}");
            }
        }

        /// <summary>
        /// Общий интерфейс адаптеров
        /// </summary>
        internal abstract class ProductAdapter
        {
            protected double _productPrice;

            protected double _productDiscount;
            
            public abstract string GetItemName();

            public abstract double GetItemPrice();

            public abstract double GetItemDiscount();

            public double GetTotalPrice()
            {
                return _productPrice * (1 - _productDiscount);
            }
        }


        /// <summary>
        /// Адаптер для товара уличной торговли
        /// </summary>
        internal class StreetTradingEntityAdapter : ProductAdapter
        {
            private StreetTradingEntity _streetTradingEntity;

            public StreetTradingEntityAdapter(StreetTradingEntity streetTradingEntity)
            {
                _streetTradingEntity = streetTradingEntity;
            }

            public override double GetItemDiscount()
            {
                _productDiscount = 0;
                return _productDiscount;
            }

            public override string GetItemName() => _streetTradingEntity.Name;

            public override double GetItemPrice()
            {
                _productPrice =  Convert.ToDouble(_streetTradingEntity.Cost);
                return _productPrice;
            }
        }

        /// <summary>
        /// Адаптер для товара супермаркета
        /// </summary>
        internal class SupermarketEntityAdapter : ProductAdapter
        {
            private SupermarketEntity _supermarketEntity;

            public SupermarketEntityAdapter(SupermarketEntity supermarketEntity)
            {
                _supermarketEntity = supermarketEntity;
            }

            public override double GetItemDiscount()
            {
                _productDiscount = _supermarketEntity.Discount;
                return _productDiscount;
            }

        public override string GetItemName() => _supermarketEntity.Name;

            public override double GetItemPrice()
            {
                _productPrice = _supermarketEntity.Price;
                return _productPrice;
            }
        }
    }
}
