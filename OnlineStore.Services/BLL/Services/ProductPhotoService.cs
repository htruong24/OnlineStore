using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Cryptography.X509Certificates;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Services.BLL.Contracts;

namespace OnlineStore.Services.BLL.Services
{
    public class ProductPhotoService: IProductPhotoService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public ProductPhotoService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ProductPhoto GetProductPhoto(int? productPhotoId)
        {
            using (_unitOfWork)
            {
                var productPhoto =
                    _unitOfWork.GetRepository<Data.Entities.ProductPhoto>()
                        .Get(x => x.Id == productPhotoId, null, "CreatedBy,ModifiedBy,Photo,Product")
                        .FirstOrDefault();
                return productPhoto;
            }
        }

        public void UpdateProductPhoto(ProductPhoto productPhoto)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.ProductPhoto>().Update(productPhoto);
                _unitOfWork.Save();
            }
        }

        public void UpdateMultipleProductPhotos(List<ProductPhoto> productPhotos, int productId)
        {
            using (_unitOfWork)
            {
                foreach (var productPhoto in productPhotos)
                {
                    if (productPhoto.Status == PhotoStatus.DELETE)
                    {
                        if (productPhoto.Id > 0)
                        {
                            _unitOfWork.GetRepository<ProductPhoto>().Delete(productPhoto.Id);
                        }
                    }
                    else
                    {
                        if(productPhoto.Id > 0)
                        {
                            _unitOfWork.GetRepository<ProductPhoto>().Update(productPhoto);
                        }
                        else
                        {
                            productPhoto.ProductId = productId;
                            _unitOfWork.GetRepository<ProductPhoto>().Create(productPhoto);
                        }
                    }
                }
                _unitOfWork.Save();
            }
        }

        public void CreateProductPhoto(ProductPhoto productPhoto, int productId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<ProductPhoto>().Create(productPhoto);
                _unitOfWork.Save();
            }
        }

        public void CreateMultipleProductPhotos(List<ProductPhoto> productPhotos, int productId)
        {
            using (_unitOfWork)
            {
                foreach (var productPhoto in productPhotos)
                {
                    if (productPhoto.Status != PhotoStatus.DELETE)
                    {
                        productPhoto.ProductId = productId;
                        _unitOfWork.GetRepository<ProductPhoto>().Create(productPhoto);
                    }
                }
                _unitOfWork.Save();
            }
        }

        public void DeleteProductPhoto(int? productPhotoId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<ProductPhoto>().Delete(productPhotoId);
                _unitOfWork.Save();
            }
        }

        public List<ProductPhoto> GetProductPhotos(int? productId)
        {
            using (_unitOfWork)
            {
                return _unitOfWork.GetRepository<ProductPhoto>().Get(null, null, "CreatedBy,ModifiedBy,Photo").Where(x => x.ProductId == productId).ToList();
            }
        }
    }
}
