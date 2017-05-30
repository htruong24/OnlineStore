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
    public class PhotoService: IPhotoService
    {
        public SortingPagingInfo Pagination;

        public DefaultFilter Filter;

        private readonly UnitOfWork _unitOfWork;

        public PhotoService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Photo GetPhoto(int? photoId)
        {
            using (_unitOfWork)
            {
                var photo =
                    _unitOfWork.GetRepository<Data.Entities.Photo>()
                        .Get(x => x.Id == photoId, null, "CreatedBy,ModifiedBy")
                        .FirstOrDefault();
                return photo;
            }
        }

        public void UpdatePhoto(Photo photo)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Photo>().Update(photo);
                _unitOfWork.Save();
            }
        }

        public void CreatePhoto(Photo photo)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Photo>().Create(photo);
                _unitOfWork.Save();
            }
        }

        public void CreateMultiplePhotos(List<Photo> photos)
        {
            using (_unitOfWork)
            {
                foreach(var photo in photos)
                {
                    _unitOfWork.GetRepository<Photo>().Create(photo);
                }
                _unitOfWork.Save();
            }
        }

        public void DeletePhoto(int? photoId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Photo>().Delete(photoId);
                _unitOfWork.Save();
            }
        }

        public List<Photo> GetCategories()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<Photo>().Get(null, null, "CreatedBy,ModifiedBy");
                
                // Sorting
                switch (Pagination.SortField)
                {
                    case "Id":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Id) :
                                 query.OrderByDescending(c => c.Id));
                        break;
                    case "Title":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Title) :
                                 query.OrderByDescending(c => c.Title));
                        break;
                    case "Description":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Description) :
                                 query.OrderByDescending(c => c.Description));
                        break;
                    case "FileSize":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.FileSize) :
                                 query.OrderByDescending(c => c.FileSize));
                        break;
                    case "Extension":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Extension) :
                                 query.OrderByDescending(c => c.Extension));
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
                                             || x.Extension.Contains(Filter.Keyword));
                }

                // Paging
                Pagination.TotalRows = query.Count();
                Pagination.PageCount =
                    (int)Math.Ceiling((double)query.Count() / Pagination.PageSize);

                var pageIndex = Pagination.CurrentPage - 1;

                query = Pagination.PageSize == 0 ? query.AsQueryable() : query.AsQueryable().Skip(pageIndex * Pagination.PageSize).Take(Pagination.PageSize);

                var categories = query.ToList();

                return categories;
            }
        }
    }
}
