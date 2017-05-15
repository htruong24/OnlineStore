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
    public class SubCategoryService: ISubCategoryService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public SubCategoryService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public SubCategory GetSubCategory(int? subCategoryId)
        {
            using (_unitOfWork)
            {
                var subCategory = _unitOfWork.GetRepository<Data.Entities.SubCategory>().GetById(subCategoryId);
                return subCategory;
            }
        }

        public void UpdateSubCategory(SubCategory subCategory)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.SubCategory>().Update(subCategory);
                _unitOfWork.Save();
            }
        }

        public void CreateSubCategory(SubCategory subCategory)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<SubCategory>().Create(subCategory);
                _unitOfWork.Save();
            }
        }

        public void DeleteSubCategory(int? subCategoryId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<SubCategory>().Delete(subCategoryId);
                _unitOfWork.Save();
            }
        }

        public List<SubCategory> GetSubCategories()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<SubCategory>().All();
                
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
