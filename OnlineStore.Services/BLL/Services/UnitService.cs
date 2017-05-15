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
    public class UnitService: IUnitService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public UnitService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Unit GetUnit(int? unitId)
        {
            using (_unitOfWork)
            {
                var unit = _unitOfWork.GetRepository<Data.Entities.Unit>().GetById(unitId);
                return unit;
            }
        }

        public void UpdateUnit(Unit unit)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Unit>().Update(unit);
                _unitOfWork.Save();
            }
        }

        public void CreateUnit(Unit unit)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Unit>().Create(unit);
                _unitOfWork.Save();
            }
        }

        public void DeleteUnit(int? unitId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Unit>().Delete(unitId);
                _unitOfWork.Save();
            }
        }

        public List<Unit> GetUnits()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Unit>().All();
                
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
                    query = query.Where(x => x.Name.Contains(Filter.Keyword)
                                             || x.Description.Contains(Filter.Keyword));
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
