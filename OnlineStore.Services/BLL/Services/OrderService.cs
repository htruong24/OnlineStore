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
                var order = _unitOfWork.GetRepository<Data.Entities.Order>()
                        .Get(x => x.Id == orderId, null, "CreatedBy,ModifiedBy")
                        .FirstOrDefault();
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

        public Order CreateOrder(Order order)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Order>().Create(order);
                _unitOfWork.Save();

                order.Code = GenerateOrderCode(order.Id);

                _unitOfWork.GetRepository<Order>().Update(order);
                _unitOfWork.Save();

                return order;
            }
        }

        public string GenerateOrderCode(int orderCode)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[10];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var randomStr = new String(stringChars);
            return randomStr + orderCode.ToString();
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
                    case "Code":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Code) :
                                 query.OrderByDescending(c => c.Code));
                        break;
                    case "CustomerName":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.CustomerName) :
                                 query.OrderByDescending(c => c.CustomerName));
                        break;
                    case "Email":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Email) :
                                 query.OrderByDescending(c => c.Email));
                        break;
                    case "Telephone":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Telephone) :
                                 query.OrderByDescending(c => c.Telephone));
                        break;
                    case "PaymentMethod":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.PaymentMethod) :
                                 query.OrderByDescending(c => c.PaymentMethod));
                        break;
                    case "DeliveryMethod":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.DeliveryMethod) :
                                 query.OrderByDescending(c => c.DeliveryMethod));
                        break;
                    case "Note":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Note) :
                                 query.OrderByDescending(c => c.Note));
                        break;
                    case "Status":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Status) :
                                 query.OrderByDescending(c => c.Status));
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
                    query = query.Where(x => x.Code.Contains(Filter.Keyword)
                                             || x.CustomerName.Contains(Filter.Keyword)
                                             || x.Email.Contains(Filter.Keyword)
                                             || x.Telephone.Contains(Filter.Keyword)
                                             || x.PaymentMethod.Contains(Filter.Keyword)
                                             || x.DeliveryMethod.Contains(Filter.Keyword)
                                             || x.Note.Contains(Filter.Keyword)
                                             || x.Status.Contains(Filter.Keyword));
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

        public Order GetOrderByEmailAndCode(string email, string code)
        {
            using (_unitOfWork)
            {
                var order = _unitOfWork.GetRepository<Data.Entities.Order>()
                        .Get(x => x.Email == email && x.Code == code, null, "CreatedBy,ModifiedBy")
                        .FirstOrDefault();
                return order;
            }
        }

        public Order GetOrderByTelephoneAndCode(string telephone, string code)
        {
            using (_unitOfWork)
            {
                var order = _unitOfWork.GetRepository<Data.Entities.Order>()
                        .Get(x => x.Telephone == telephone && x.Code == code, null, "CreatedBy,ModifiedBy")
                        .FirstOrDefault();
                return order;
            }
        }
    }
}
