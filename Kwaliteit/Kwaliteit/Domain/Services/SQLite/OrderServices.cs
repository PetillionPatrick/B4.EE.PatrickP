using Kwaliteit.Domain.Models;
using Kwaliteit.Domain.Services.Abstract;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Kwaliteit.Domain.Services.SQLite
{
    public class OrderServices : SQLiteServiceBase, IOrderServices
    {
        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            return await Task.Run<Order>(() =>
            {
                try
                {
                    var order = connection.Find<Order>(o => o.Id == orderId);

                    return order;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveOrder(Order order, bool isNewItem = false)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.InsertOrReplace(order);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }
    }
}
