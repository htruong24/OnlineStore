﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Services.BLL.Contracts;

namespace OnlineStore.Services.BLL.Services
{
    public class AddressService: IAddressService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public AddressService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Address GetAddress(int? addressId)
        {
            using (_unitOfWork)
            {
                var address = _unitOfWork.GetRepository<Data.Entities.Address>()
                     .Get(x => x.Id == addressId, null, "CreatedBy,ModifiedBy")
                     .FirstOrDefault();

                return address;
            }
        }

        public void UpdateAddress(Address address)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Address>().Update(address);
                _unitOfWork.Save();
            }
        }

        public void CreateAddress(Address address)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Address>().Create(address);
                _unitOfWork.Save();
            }
        }

        public void DeleteAddress(int? addressId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Address>().Delete(addressId);
                _unitOfWork.Save();
            }
        }

        public List<Address> GetAddresses()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Address>().Get(null, null, "CreatedBy,ModifiedBy");

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
    }
}