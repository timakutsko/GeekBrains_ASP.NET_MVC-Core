using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.DAL.Entities
{
    [Table("Products")]
    public class Product : AbsNamedEntity
    {
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

#nullable enable
        public string? Category { get; set; }
    }
}
