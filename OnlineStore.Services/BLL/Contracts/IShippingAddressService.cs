using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IShippingAddressService
    {
        ShippingAddress GetShippingAddress(int? shippingAddressId);
        void UpdateShippingAddress(ShippingAddress address);
        void CreateShippingAddress(ShippingAddress address);
        void DeleteShippingAddress(int? shippingAddressId);
        List<ShippingAddress> GetShippingAddresses();
        ShippingAddress GetShippingAddressByCustomerId(int? customerId);
    }
}
