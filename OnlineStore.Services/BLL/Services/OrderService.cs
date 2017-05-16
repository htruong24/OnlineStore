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
    public class OrderService: IOrderService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public OrderService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Order GetOrder(int? orderId)
        {
            using (_unitOfWork)
            {
                var order = _unitOfWork.GetRepository<Data.Entities.Order>().GetById(orderId);
                return order;
            }
        }

        public void UpdateOrder(Order order)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Order>().Update(order);
                _unitOfWork.Save();
            }
        }

        public void CreateOrder(Order order)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Order>().Create(order);
                _unitOfWork.Save();
            }
        }

        public void DeleteOrder(int? orderId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Order>().Delete(orderId);
                _unitOfWork.Save();
            }
        }

        public List<Order> GetOrders()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Order>().All();
                
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
                                 query.OrderBy(c => c.CreatedBy) :
                                 query.OrderByDescending(c => c.CreatedBy));
                        break;
                    case "ModifiedOn":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.ModifiedOn) :
                                 query.OrderByDescending(c => c.ModifiedOn));
                        break;
                    case "ModifiedBy":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.ModifiedBy) :
                                 query.OrderByDescending(c => c.ModifiedBy));
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
