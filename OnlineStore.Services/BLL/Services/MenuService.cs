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
    public class MenuService: IMenuService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public MenuService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Menu GetMenu(int? menuId)
        {
            using (_unitOfWork)
            {
                var menu = _unitOfWork.GetRepository<Data.Entities.Menu>()
                        .Get(x => x.Id == menuId, null, "CreatedBy,ModifiedBy")
                        .FirstOrDefault();
                return menu;
            }
        }

        public void UpdateMenu(Menu menu)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Menu>().Update(menu);
                _unitOfWork.Save();
            }
        }

        public void CreateMenu(Menu menu)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Menu>().Create(menu);
                _unitOfWork.Save();
            }
        }

        public void DeleteMenu(int? menuId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Menu>().Delete(menuId);
                _unitOfWork.Save();
            }
        }

        public List<Menu> GetMenus()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Menu>().Get(null, null, "CreatedBy,ModifiedBy");
                
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
                            ? query.OrderBy(c => c.Title)
                            : query.OrderByDescending(c => c.Title));
                        break;
                    case "Description":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Description) :
                                 query.OrderByDescending(c => c.Description));
                        break;
                    case "Url":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Url) :
                                 query.OrderByDescending(c => c.Url));
                        break;
                    case "OrderNumber":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.OrderNumber) :
                                 query.OrderByDescending(c => c.OrderNumber));
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
                    query = query.Where(x => x.Title.Contains(Filter.Keyword)
                                             || x.Description.Contains(Filter.Keyword)
                                             || x.Url.Contains(Filter.Keyword));
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
