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
    public class BrandService: IBrandService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public BrandService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Brand GetBrand(int? brandId)
        {
            using (_unitOfWork)
            {
                var brand = _unitOfWork.GetRepository<Data.Entities.Brand>()
                        .Get(x => x.Id == brandId, null, "CreatedBy,ModifiedBy")
                        .FirstOrDefault();
                var subCategoryIds = Array.ConvertAll(brand.SubCategoryIds.Split(','), x => int.Parse(x));
                var subcategories = new List<SubCategory>();
                foreach (var id in subCategoryIds)
                {
                    var subcategory = _unitOfWork.GetRepository<SubCategory>().GetById(id);
                    subcategories.Add(subcategory);
                }
                brand.SubCategoryList = subcategories;
                brand.SubCategoryDisplayText = string.Join(", ", subcategories.Select(x => "<a target='_blank' href='/danh-muc-con/" + x.MetaTitle + "-" + x.Id + "'>" + x.Name + "</a>").ToList()); 

                return brand;
            }
        }

        public void UpdateBrand(Brand brand)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Brand>().Update(brand);
                _unitOfWork.Save();
            }
        }

        public void CreateBrand(Brand brand)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Brand>().Create(brand);
                _unitOfWork.Save();
            }
        }

        public void DeleteBrand(int? brandId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Brand>().Delete(brandId);
                _unitOfWork.Save();
            }
        }

        public List<Brand> GetBrands()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Brand>().Get(null, null, "CreatedBy,ModifiedBy");

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
                                             || x.Description.Contains(Filter.Keyword));
                }
                // Fitler
                if (Filter != null)
                {
                    // Filter by keyword
                    if (Filter.SubCategoryId > 0)
                    {
                        query = query.Where(x => x.SubCategoryIds.StartsWith(Filter.SubCategoryId.ToString() + ",") || x.SubCategoryIds.EndsWith("," + Filter.SubCategoryId.ToString()) || x.SubCategoryIds.Contains("," + Filter.SubCategoryId.ToString() + ",") || x.SubCategoryIds == Filter.SubCategoryId.ToString());
                    }
                    if (Filter.CategoryId > 0)
                    {
                        var category = _unitOfWork.GetRepository<Category>().Get(x => x.Id == Filter.CategoryId, null, "SubCategories").FirstOrDefault();
                        var subCategoryList = category.SubCategories.Select(x => x.Id).ToList();
                        query = query.Where(x => subCategoryList.Any(y => x.SubCategoryIds.StartsWith(y + ",")) || subCategoryList.Any(z => x.SubCategoryIds.EndsWith("," + z)) || subCategoryList.Any(h => x.SubCategoryIds.Contains("," + h + ",")) || subCategoryList.Any(l => x.SubCategoryIds == l.ToString()));
                    }
                }

                // Paging
                Pagination.TotalRows = query.Count();
                Pagination.PageCount =
                    (int)Math.Ceiling((double)query.Count() / Pagination.PageSize);

                var pageIndex = Pagination.CurrentPage - 1;
                query = Pagination.PageSize == 0 ? query.AsQueryable() : query.AsQueryable().Skip(pageIndex * Pagination.PageSize).Take(Pagination.PageSize);

                var results = query.ToList();
                foreach (var brand in results)
                {
                    if (brand.SubCategoryIds != null)
                    {
                        var subCategoryIds = Array.ConvertAll(brand.SubCategoryIds.Split(','), x => int.Parse(x));
                        var subcategories = new List<SubCategory>();
                        foreach (var id in subCategoryIds)
                        {
                            var subcategory = _unitOfWork.GetRepository<SubCategory>().GetById(id);
                            subcategories.Add(subcategory);
                        }
                        brand.SubCategoryList = subcategories;
                        brand.SubCategoryDisplayText = string.Join(", ", subcategories.Select(x => "<a target='_blank' href='/danh-muc-con/" + x.MetaTitle + "-" + x.Id + "'>" + x.Name + "</a>").ToList());
                    }
                }
                return results;
            }
        }
    }
}
