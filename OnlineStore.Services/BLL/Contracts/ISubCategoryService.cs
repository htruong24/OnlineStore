using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface ISubCategoryService
    {
        SubCategory GetSubCategory(int? subCategoryId);
        void UpdateSubCategory(SubCategory subCategory);
        void CreateSubCategory(SubCategory subCategory);
        void DeleteSubCategory(int? subCategoryId);
        List<SubCategory> GetSubCategories();
    }
}
