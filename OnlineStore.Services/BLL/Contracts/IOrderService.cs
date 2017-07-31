using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IOrderService
    {
        Order GetOrder(int? orderId);
        Order GetOrderByEmailAndCode(string email, string code);
        Order GetOrderByTelephoneAndCode(string telephone, string code);
        void UpdateOrder(Order order);
        Order CreateOrder(Order order);
        void DeleteOrder(int? orderId);
        List<Order> GetOrders();
    }
}
