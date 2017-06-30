using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Services.BLL.Contracts;

namespace OnlineStore.Services.BLL.Services
{
    public class ProductService: IProductService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public ProductService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Product GetProduct(int? productId)
        {
            using (_unitOfWork)
            {
                var product = _unitOfWork.GetRepository<Data.Entities.Product>()
                        .Get(x => x.Id == productId, null, "CreatedBy,ModifiedBy,Unit,Brand,SubCategory,ProductPhotos")
                        .FirstOrDefault();
                
                return product;
            }
        }

        public void UpdateProduct(Product product)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Product>().Update(product);
                _unitOfWork.Save();
            }
        }

        public Product CreateProduct(Product product)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Product>().Create(product);
                _unitOfWork.Save();
                return product;
            }
        }

        public void DeleteProduct(int? productId)
        {
            using (_unitOfWork)
            {
                var product = _unitOfWork.GetRepository<Data.Entities.Product>()
                        .Get(x => x.Id == productId, null, "CreatedBy,ModifiedBy,Unit,Brand,SubCategory,ProductPhotos")
                        .FirstOrDefault();

                var productPhotos = product.ProductPhotos.ToList();

                for (var i = 0; i < productPhotos.Count; i++)
                {
                   _unitOfWork.GetRepository<ProductPhoto>().Delete(productPhotos[i].Id);
                }

                _unitOfWork.GetRepository<Product>().Delete(productId);
                _unitOfWork.Save();
            }
        }

        public List<Product> GetProducts()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Product>().Get(null, null, "CreatedBy,ModifiedBy,Unit,Brand,SubCategory,SubCategory.Category,ProductPhotos,ProductPhotos.Photo");
                
                // Sorting
                switch (Pagination.SortField)
                {
                    case "Id":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Id) :
                                 query.OrderByDescending(c => c.Id));
                        break;
                    case "Name":
                        query = (Pagination.SortDirection == "ascending"
                            ? query.OrderBy(c => c.Name)
                            : query.OrderByDescending(c => c.Name));
                        break;
                    case "ShortDescrition":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.ShortDescrition) :
                                 query.OrderByDescending(c => c.ShortDescrition));
                        break;
                    case "Description":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Description) :
                                 query.OrderByDescending(c => c.Description));
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
                if (Filter != null)
                {
                    // Filter by keyword
                    if (!string.IsNullOrEmpty(Filter.Keyword))
                    {
                        query = query.Where(x => x.Name.Contains(Filter.Keyword)
                                                 || x.ShortDescrition.Contains(Filter.Keyword)
                                                 || x.Description.Contains(Filter.Keyword));
                    }
                    if (Filter.CategoryId > 0)
                    {
                        query = query.Where(x => x.SubCategory.Category.Id == Filter.CategoryId);
                    }
                    if (Filter.SubCategoryId > 0)
                    {
                        query = query.Where(x => x.SubCategory.Id == Filter.SubCategoryId);
                    }
                }

                // Paging
                Pagination.TotalRows = query.Count();
                Pagination.PageCount =
                    (int)Math.Ceiling((double)query.Count() / Pagination.PageSize);

                var pageIndex = Pagination.CurrentPage - 1;

                query = Pagination.PageSize == 0 ? query.AsQueryable() : query.AsQueryable().Skip(pageIndex * Pagination.PageSize).Take(Pagination.PageSize);

                return query.ToList();

            }
        }
    }
}
