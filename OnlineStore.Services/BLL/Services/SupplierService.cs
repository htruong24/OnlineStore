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
    public class SupplierService: ISupplierService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public SupplierService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Supplier GetSupplier(int? supplierId)
        {
            using (_unitOfWork)
            {
                var supplier = _unitOfWork.GetRepository<Data.Entities.Supplier>()
                        .Get(x => x.Id == supplierId, null, "CreatedBy,ModifiedBy")
                        .FirstOrDefault();
                return supplier;
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Supplier>().Update(supplier);
                _unitOfWork.Save();
            }
        }

        public void CreateSupplier(Supplier supplier)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Supplier>().Create(supplier);
                _unitOfWork.Save();
            }
        }

        public void DeleteSupplier(int? supplierId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Supplier>().Delete(supplierId);
                _unitOfWork.Save();
            }
        }

        public List<Supplier> GetSuppliers()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Supplier>().Get(null, null, "CreatedBy,ModifiedBy");
                
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
                    case "Address":
                        query = (Pagination.SortDirection == "ascending"
                            ? query.OrderBy(c => c.Address)
                            : query.OrderByDescending(c => c.Address));
                        break;
                    case "Telephone":
                        query = (Pagination.SortDirection == "ascending"
                            ? query.OrderBy(c => c.Telephone)
                            : query.OrderByDescending(c => c.Telephone));
                        break;
                    case "CellPhone":
                        query = (Pagination.SortDirection == "ascending"
                            ? query.OrderBy(c => c.CellPhone)
                            : query.OrderByDescending(c => c.CellPhone));
                        break;
                    case "Email":
                        query = (Pagination.SortDirection == "ascending"
                            ? query.OrderBy(c => c.Email)
                            : query.OrderByDescending(c => c.Email));
                        break;
                    case "Fax":
                        query = (Pagination.SortDirection == "ascending"
                            ? query.OrderBy(c => c.Fax)
                            : query.OrderByDescending(c => c.Fax));
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
                if (!string.IsNullOrEmpty(Filter?.Keyword))
                {
                    query = query.Where(x => x.Name.Contains(Filter.Keyword)
                                             || x.Description.Contains(Filter.Keyword)
                                             || x.Address.Contains(Filter.Keyword)
                                             || x.Telephone.Contains(Filter.Keyword)
                                             || x.CellPhone.Contains(Filter.Keyword)
                                             || x.Email.Contains(Filter.Keyword)
                                             || x.Fax.Contains(Filter.Keyword));
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
