using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.Abstract
{
    public interface IOrderServices
    {
        Task<Order> GetOrderAsync(Guid orderId);
        Task SaveOrder(Order order, bool isNewItem = false);
    }
}
