using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IProductPhotoService
    {
        ProductPhoto GetProductPhoto(int? productPhotoId);
        void CreateProductPhoto(ProductPhoto productPhoto, int productId);
        void CreateMultipleProductPhotos(List<ProductPhoto> productPhotos, int productId);
        void UpdateProductPhoto(ProductPhoto productPhoto);
        void UpdateMultipleProductPhotos(List<ProductPhoto> productPhotos, int productId);
        void DeleteProductPhoto(int? productPhotoId);
        List<ProductPhoto> GetProductPhotos(int? productId);
    }
}
