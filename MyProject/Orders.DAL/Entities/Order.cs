using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.DAL.Entities
{
    [Table("Oders")]
    public class Order :AbsEntity
    {
        public DateTime OrderDate { get; set; }

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public Buyer Buyer { get; set; } = null!;

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
    }
}
