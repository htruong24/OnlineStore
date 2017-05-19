using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Cryptography.X509Certificates;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Services.BLL.Contracts;

namespace OnlineStore.Services.BLL.Services
{
    public class CategoryService: ICategoryService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Category GetCategory(int? categoryId)
        {
            using (_unitOfWork)
            {
                var category = _unitOfWork.GetRepository<Data.Entities.Category>().GetById(categoryId);
                category.Creator = GetReferenceUser(category.CreatedBy);
                category.Modifier = GetReferenceUser(category.ModifiedBy);
                return category;
            }
        }

        public void UpdateCategory(Category category)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Category>().Update(category);
                _unitOfWork.Save();
            }
        }

        public void CreateCategory(Category category)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Category>().Create(category);
                _unitOfWork.Save();
            }
        }

        public void DeleteCategory(int? categoryId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Category>().Delete(categoryId);
                _unitOfWork.Save();
            }
        }

        public List<Category> GetCategories()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Category>().All();
                
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

                var categories = query.ToList();

                foreach (var category in categories)
                {
                    category.Creator = GetReferenceUser(category.CreatedBy);
                    category.Modifier = GetReferenceUser(category.ModifiedBy);
                }

                return categories;
            }
        }

        public User GetReferenceUser(object userId)
        {
            return _unitOfWork.GetRepository<User>().GetById(userId);
        }
    }
}
