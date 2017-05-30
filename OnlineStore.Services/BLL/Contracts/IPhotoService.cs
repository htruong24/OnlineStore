using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IPhotoService
    {
        Photo GetPhoto(int? photoId);
        void UpdatePhoto(Photo photo);
        void CreatePhoto(Photo photo);
        void CreateMultiplePhotos(List<Photo> photos);
        void DeletePhoto(int? photoId);
        List<Photo> GetCategories();
    }
}
