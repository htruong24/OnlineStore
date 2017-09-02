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
    public class CountryService: ICountryService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public CountryService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Country GetCountry(int? countryId)
        {
            using (_unitOfWork)
            {
                var country = _unitOfWork.GetRepository<Data.Entities.Country>()
                     .Get(x => x.Id == countryId, null, "CreatedBy,ModifiedBy")
                     .FirstOrDefault();

                return country;
            }
        }

        public void UpdateCountry(Country country)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Country>().Update(country);
                _unitOfWork.Save();
            }
        }

        public void CreateCountry(Country country)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Country>().Create(country);
                _unitOfWork.Save();
            }
        }

        public void DeleteCountry(int? countryId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Country>().Delete(countryId);
                _unitOfWork.Save();
            }
        }

        public List<Country> GetCountries()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Country>().Get(null, null, "CreatedBy,ModifiedBy");

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
                    query = query.Where(x => x.Name.Contains(Filter.Keyword));
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
