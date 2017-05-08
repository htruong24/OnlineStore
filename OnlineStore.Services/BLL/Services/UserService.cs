using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Services.BLL.Contracts;

namespace OnlineStore.Services.BLL.Services
{
    public class UserService: IUserService
    {
        public SortingPagingInfo Pagination;

        public string ErrorMessage;

        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public User GetUser(string userId)
        {
            using (_unitOfWork)
            {
                var user = _unitOfWork.GetRepository<Data.Entities.User>().GetById(userId);
                return user;
            }
        }

        public void UpdateUser(User user)
        {
            using (_unitOfWork)
            {
                var currentUser = _unitOfWork.GetRepository<User>().Filter(x => x.Id == user.Id).First();
                user.Password = currentUser.Password;
                _unitOfWork.GetRepository<Data.Entities.User>().Update(user);
                _unitOfWork.Save();
            }
        }

        public void CreateUser(User user)
        {
            using (_unitOfWork)
            {
                var automaticValueService = new AutomaticValueService(_unitOfWork);
                // Get New Id and Password
                user.Id = automaticValueService.GetNextId(AutomaticTable.USER);
                user.Password = Encryptor.MD5Hash(CommonConstants.DEFAULT_PASSWORD);
                // Create new User
                _unitOfWork.GetRepository<User>().Create(user);
                _unitOfWork.Save();
                //Update Automatic Value
                var currentAutomaticValue = automaticValueService.GetAutomaticValue(AutomaticTable.USER);
                currentAutomaticValue.LastValue = user.Id;
                automaticValueService.UpdateAutomaticValue(currentAutomaticValue);
            }
        }

        public void DeleteUser(string userId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<User>().Delete(userId);
                _unitOfWork.Save();
            }
        }

        public List<User> GetUsers()
        {
            using (_unitOfWork)
            {
                var query = _unitOfWork.GetRepository<User>().All();

                switch (Pagination.SortField)
                {
                    case "Id":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Id) :
                                 query.OrderByDescending(c => c.Id));
                        break;
                    case "Code":
                        query = (Pagination.SortDirection == "ascending"
                            ? query.OrderBy(c => c.Username)
                            : query.OrderByDescending(c => c.Username));
                        break;
                    case "FirstName":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.FirstName) :
                                 query.OrderByDescending(c => c.FirstName));
                        break;
                    case "LastName":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.LastName) :
                                 query.OrderByDescending(c => c.LastName));
                        break;
                    case "FullName":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.FullName) :
                                 query.OrderByDescending(c => c.FullName));
                        break;
                    case "Gender":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Gender) :
                                 query.OrderByDescending(c => c.Gender));
                        break;
                    case "Address":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Address) :
                                 query.OrderByDescending(c => c.Address));
                        break;
                    case "Email":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Email) :
                                 query.OrderByDescending(c => c.Email));
                        break;
                    case "Telephone":
                        query = (Pagination.SortDirection == "ascending" ?
                                 query.OrderBy(c => c.Telephone) :
                                 query.OrderByDescending(c => c.Telephone));
                        break;
                }

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
