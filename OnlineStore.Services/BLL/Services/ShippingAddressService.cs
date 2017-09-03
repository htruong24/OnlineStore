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
    public class ShippingAddressService: IShippingAddressService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public ShippingAddressService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ShippingAddress GetShippingAddress(int? shippingAddressId)
        {
            using (_unitOfWork)
            {
                var shippingAddress = _unitOfWork.GetRepository<Data.Entities.ShippingAddress>()
                     .Get(x => x.Id == shippingAddressId, null, "City,Customer,CreatedBy,ModifiedBy")
                     .FirstOrDefault();

                return shippingAddress;
            }
        }

        public void UpdateShippingAddress(ShippingAddress shippingAddress)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.ShippingAddress>().Update(shippingAddress);
                _unitOfWork.Save();
            }
        }

        public void CreateShippingAddress(ShippingAddress shippingAddress)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<ShippingAddress>().Create(shippingAddress);
                _unitOfWork.Save();
            }
        }

        public void DeleteShippingAddress(int? shippingAddressId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<ShippingAddress>().Delete(shippingAddressId);
                _unitOfWork.Save();
            }
        }

        public List<ShippingAddress> GetShippingAddresses()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<ShippingAddress>().Get(null, null, "City,Customer,CreatedBy,ModifiedBy");

                // Sorting
                switch (Pagination.SortField)
                {
                    case "Id":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Id) :
                                 query.OrderByDescending(c => c.Id));
                        break;
                    case "Value":
                        query = (Pagination.SortDirection == "ascending"
                            ? query.OrderBy(c => c.Value)
                            : query.OrderByDescending(c => c.Value));
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
                    query = query.Where(x => x.Value.Contains(Filter.Keyword)
                                             || x.Note.Contains(Filter.Keyword));
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

        public ShippingAddress GetShippingAddressByCustomerId(int? customerId)
        {
            using (_unitOfWork)
            {
                var shippingAddress = _unitOfWork.GetRepository<Data.Entities.ShippingAddress>()
                     .Get(x => x.CustomerId == customerId, null, "City,Customer,CreatedBy,ModifiedBy")
                     .FirstOrDefault();

                return shippingAddress;
            }
        }
    }
}
