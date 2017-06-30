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
    public class RecommendProductService: IRecommendProductService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public RecommendProductService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public RecommendProduct GetRecommendProduct(int? recommendProductId)
        {
            using (_unitOfWork)
            {
                var recommendProduct = _unitOfWork.GetRepository<Data.Entities.RecommendProduct>()
                        .Get(x => x.Id == recommendProductId, null, "")
                        .FirstOrDefault();
                return recommendProduct;
            }
        }

        public RecommendProduct GetRecommendProductByProductId(int? productId)
        {
            using (_unitOfWork)
            {
                var recommendProduct = _unitOfWork.GetRepository<Data.Entities.RecommendProduct>()
                        .Get(x => x.ProductId == productId, null, "")
                        .FirstOrDefault();
                return recommendProduct;
            }
        }

        public void UpdateRecommendProduct(RecommendProduct recommendProduct)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.RecommendProduct>().Update(recommendProduct);
                _unitOfWork.Save();
            }
        }

        public void CreateRecommendProduct(RecommendProduct recommendProduct)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<RecommendProduct>().Create(recommendProduct);
                _unitOfWork.Save();
            }
        }

        public void DeleteRecommendProduct(int? recommendProductId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<RecommendProduct>().Delete(recommendProductId);
                _unitOfWork.Save();
            }
        }

        public List<RecommendProduct> GetRecommendProducts()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<RecommendProduct>().Get(null, null, "");
                
                // Sorting
                switch (Pagination.SortField)
                {
                    case "Id":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Id) :
                                 query.OrderByDescending(c => c.Id));
                        break;
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
