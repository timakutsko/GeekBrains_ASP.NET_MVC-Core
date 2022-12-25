using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.DAL.Entities
{
    [Table("Buyers")]
    public class Buyer : AbsNamedEntity
    {
#nullable enable
        public string? LastName { get; set; }

#nullable enable
        public string? Patronymic { get; set; }

        public DateTime Birthday { get; set; }
    }
}
