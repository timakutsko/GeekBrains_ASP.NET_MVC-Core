using Orders.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models.Reports
{
    internal class OrderCatalog : ICatalog
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
