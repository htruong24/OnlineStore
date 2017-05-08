using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Data.Interfaces;
using OnlineStore.Services.BLL.Services;

namespace OnlineStore.Services.BLL.BL
{
    public class UserBL
    {
        private readonly UserService _userService;

        public SortingPagingInfo Pagination;

        public string ErrorMessage;

        public UserBL()
        {
            this._userService = new UserService(new UnitOfWork(new DbContextFactory<OnlineStoreDbContext>()));
        }
    }
}
