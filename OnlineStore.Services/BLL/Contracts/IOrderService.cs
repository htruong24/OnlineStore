using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IOrderService
    {
        Order GetOrder(int? orderId);
        void UpdateOrder(Order order);
        void CreateOrder(Order order);
        void DeleteOrder(int? orderId);
        List<Order> GetOrders();
    }
}
