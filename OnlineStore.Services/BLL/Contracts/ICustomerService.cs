using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface ICustomerService
    {
        Customer GetCustomer(int? customerId);
        void UpdateCustomer(Customer customer);
        void CreateCustomer(Customer customer);
        void DeleteCustomer(int? customerId);
        List<Customer> GetCustomers();
    }
}
