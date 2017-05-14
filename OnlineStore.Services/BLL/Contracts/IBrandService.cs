using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IBrandService
    {
        Brand GetBrand(int? brandId);
        void UpdateBrand(Brand brand);
        void CreateBrand(Brand brand);
        void DeleteBrand(int? brandId);
        List<Brand> GetBrands();
    }
}
