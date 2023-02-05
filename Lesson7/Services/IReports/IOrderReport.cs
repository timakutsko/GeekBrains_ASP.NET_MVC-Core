using Orders.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson7.Services.IReports
{
    internal interface IOrderReport
    {
        ICollection<OrderItem> Items { get; set; }
    }
}
