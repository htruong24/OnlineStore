using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IAddressService
    {
        Address GetAddress(int? addressId);
        void UpdateAddress(Address address);
        void CreateAddress(Address address);
        void DeleteAddress(int? addressId);
        List<Address> GetAddresss();
    }
}
