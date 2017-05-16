using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IProductService
    {
        Product GetProduct(int? productId);
        void UpdateProduct(Product product);
        void CreateProduct(Product product);
        void DeleteProduct(int? productId);
        List<Product> GetProducts();
    }
}
