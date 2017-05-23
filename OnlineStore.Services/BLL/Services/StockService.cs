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
    public class StockService: IStockService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public StockService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Stock GetStock(int? stockId)
        {
            using (_unitOfWork)
            {
                var stock = _unitOfWork.GetRepository<Data.Entities.Stock>().GetById(stockId);
                return stock;
            }
        }

        public void UpdateStock(Stock stock)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Stock>().Update(stock);
                _unitOfWork.Save();
            }
        }

        public void CreateStock(Stock stock)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Stock>().Create(stock);
                _unitOfWork.Save();
            }
        }

        public void DeleteStock(int? stockId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Stock>().Delete(stockId);
                _unitOfWork.Save();
            }
        }

        public List<Stock> GetStocks()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Stock>().All();
                
                // Sorting
                switch (Pagination.SortField)
                {
                    case "Id":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Id) :
                                 query.OrderByDescending(c => c.Id));
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
                    //                         || x.ShortDescrition.Contains(Filter.Keyword)
                    //                         || x.Description.Contains(Filter.Keyword));
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
