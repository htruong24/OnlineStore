using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IFunctionService
    {
        Function GetFunction(string functionId);
        void UpdateFunction(Function function);
        void CreateFunction(Function function);
        void DeleteFunction(string functionId);
        List<Function> GetFunctions();
        List<Function> GetFunctionsByModule(string moduleId);
    }
}
