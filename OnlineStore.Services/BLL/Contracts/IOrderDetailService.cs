using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IOrderDetailService
    {
        OrderDetail GetOrderDetail(int? orderDetailId);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void CreateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(int? orderDetailId);
        List<OrderDetail> GetOrderDetails();
    }
}
