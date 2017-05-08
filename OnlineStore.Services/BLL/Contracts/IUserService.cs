using System.Collections.Generic;
using OnlineStore.Common;
using OnlineStore.Data.Entities;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IUserService
    {
        User GetUser(string userId);
        void UpdateUser(User user);
        void CreateUser(User user);
        void DeleteUser(string userId);
        List<User> GetUsers();
    }
}
