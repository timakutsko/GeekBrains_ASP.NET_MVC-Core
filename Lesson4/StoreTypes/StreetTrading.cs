using System.Collections.Generic;

namespace Lesson4.StoreTypes
{
    internal class StreetTrading
    {
        public List<StreetTradingFruit> ProductFruits { get; set; }

        public List<StreetTradingVegetable> ProductVegetables { get; set; }
    }

    internal class StreetTradingEntity
    {
        public string Name { get; set; }

        public int Cost { get; set; }
    }

    internal class StreetTradingFruit : StreetTradingEntity
    {
    }

    internal class StreetTradingVegetable : StreetTradingEntity
    {
    }
}
