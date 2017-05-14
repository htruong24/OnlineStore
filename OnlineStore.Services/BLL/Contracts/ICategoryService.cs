using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface ICategoryService
    {
        Category GetCategory(int? categoryId);
        void UpdateCategory(Category category);
        void CreateCategory(Category category);
        void DeleteCategory(int? categoryId);
        List<Category> GetCategories();
    }
}
