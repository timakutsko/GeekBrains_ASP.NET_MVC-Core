using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            // путешественник
            Driver driver = new Driver();
            // машина
            Auto auto = new Auto();
            // отправляемся в путешествие
            driver.Travel(auto);
            // встретились пески, надо использовать верблюда
            Camel camel = new Camel();
            // используем адаптер
            ITransport camelTransport = new CamelToTransportAdapter(camel);
            // продолжаем путь по пескам пустыни
            driver.Travel(camelTransport);

            Console.Read();
        }
    }
    interface ITransport
    {
        void Drive();
    }
    // класс машины
    class Auto : ITransport
    {
        public void Drive()
        {
            Console.WriteLine("Машина едет по дороге");
        }
    }
    class Driver
    {
        public void Travel(ITransport transport)
        {
            transport.Drive();
        }
    }
    // интерфейс животного
    interface IAnimal
    {
        void Move();
    }
    // класс верблюда
    class Camel : IAnimal
    {
        public void Move()
        {
            Console.WriteLine("Верблюд идет по пескам пустыни");
        }
    }
    // Адаптер от Camel к ITransport
    class CamelToTransportAdapter : ITransport
    {
        Camel camel;
        public CamelToTransportAdapter(Camel c)
        {
            camel = c;
        }

        public void Drive()
        {
            camel.Move();
        }
    }
}
