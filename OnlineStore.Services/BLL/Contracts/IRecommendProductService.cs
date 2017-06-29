using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IRecommendProductService
    {
        RecommendProduct GetRecommendProduct(int? recommendProductId);
        void UpdateRecommendProduct(RecommendProduct recommendProduct);
        void CreateRecommendProduct(RecommendProduct recommendProduct);
        void DeleteRecommendProduct(int? recommendProductId);
        List<RecommendProduct> GetRecommendProducts();
    }
}
