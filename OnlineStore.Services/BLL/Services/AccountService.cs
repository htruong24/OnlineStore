using System.Linq;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Services.BLL.Contracts;

namespace OnlineStore.Services.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _unitOfWork;

        public AccountService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public User GetUser(string username, string password)
        {
            var user = _unitOfWork.GetRepository<User>().Filter(x => x.Username == username && x.Password == password).ToList().FirstOrDefault();
            return user;
        }
    }

}
