using OnlineStore.Data.Entities;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IAccountService
    {
        User GetUser(string username, string password);
    }
}
