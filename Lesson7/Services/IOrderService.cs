using Orders.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson7.Services
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(int buyerId, string address, string phone, IEnumerable<(int productId, int quantity)> products);
    }
}
