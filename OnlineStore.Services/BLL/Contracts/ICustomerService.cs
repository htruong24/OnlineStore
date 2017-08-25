using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface ICustomerService
    {
        Customer GetCustomer(int? customerId);
        Customer GetCustomer(string email, string password);
        void UpdateCustomer(Customer customer);
        void CreateCustomer(Customer customer);
        void DeleteCustomer(int? customerId);
        List<Customer> GetCustomers();
    }
}
