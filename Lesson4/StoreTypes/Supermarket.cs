using System.Collections.Generic;

namespace Lesson4.StoreTypes
{
    internal class Supermarket
    {
        public List<SupermarketFruit> Fruits { get; set; }

        public List<SupermarketVegetable> Vegetables { get; set; }

        public List<SupermarketToy> Toys { get; set; }
    }

    internal class SupermarketEntity
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }
    }

    internal class SupermarketFruit : SupermarketEntity
    {
    }

    internal class SupermarketVegetable : SupermarketEntity
    {
    }
    
    internal class SupermarketToy : SupermarketEntity
    {
    }
}
