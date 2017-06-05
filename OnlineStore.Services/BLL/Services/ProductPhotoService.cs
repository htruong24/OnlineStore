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

        public void CreateProductPhoto(ProductPhoto productPhoto)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<ProductPhoto>().Create(productPhoto);
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

        public List<ProductPhoto> GetProductPhotos()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<ProductPhoto>().Get(null, null, "CreatedBy,ModifiedBy,Photo,Product");
                
                // Sorting
                switch (Pagination.SortField)
                {
                    case "Id":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Id) :
                                 query.OrderByDescending(c => c.Id));
                        break;
                    case "OrderNumber":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.OrderNumber) :
                                 query.OrderByDescending(c => c.OrderNumber));
                        break;
                    case "CreatedOn":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.CreatedOn) :
                                 query.OrderByDescending(c => c.CreatedOn));
                        break;
                    case "CreatedBy":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.CreatedBy.Name) :
                                 query.OrderByDescending(c => c.CreatedBy.Name));
                        break;
                    case "ModifiedOn":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.ModifiedOn) :
                                 query.OrderByDescending(c => c.ModifiedOn));
                        break;
                    case "ModifiedBy":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.ModifiedBy.Name) :
                                 query.OrderByDescending(c => c.ModifiedBy.Name));
                        break;
                }

                // Fitler
                if (!string.IsNullOrEmpty(Filter?.Keyword))
                {
                    //query = query.Where(x => x.Name.Contains(Filter.Keyword)
                    //                         || x.Description.Contains(Filter.Keyword));
                }

                // Paging
                Pagination.TotalRows = query.Count();
                Pagination.PageCount =
                    (int)Math.Ceiling((double)query.Count() / Pagination.PageSize);

                var pageIndex = Pagination.CurrentPage - 1;

                query = Pagination.PageSize == 0 ? query.AsQueryable() : query.AsQueryable().Skip(pageIndex * Pagination.PageSize).Take(Pagination.PageSize);

                var categories = query.ToList();

                return categories;
            }
        }
    }
}
