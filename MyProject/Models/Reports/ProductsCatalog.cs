using Orders.DAL.Entities;
using System;
using System.Collections.Generic;

namespace MyProject.Models.Reports
{
    public class ProductsCatalog : ICatalog
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
