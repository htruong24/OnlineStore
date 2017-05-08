using System.Collections.Generic;
using System.Linq;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Services.BLL.Contracts;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Services
{
    public class FunctionService: IFunctionService
    {
        private readonly UnitOfWork _unitOfWork;

        public FunctionService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Function GetFunction(string functionId)
        {
            using (_unitOfWork)
            {
                var function = _unitOfWork.GetRepository<Data.Entities.Function>().GetById(functionId);
                return function;
            }
        }

        public void UpdateFunction(Function function)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Function>().Update(function);
                _unitOfWork.Save();
            }
        }

        public void CreateFunction(Function function)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Function>().Create(function);
                _unitOfWork.Save();
            }
        }

        public void DeleteFunction(string functionId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Function>().Delete(functionId);
                _unitOfWork.Save();
            }
        }

        public List<Function> GetFunctions()
        {
            using (_unitOfWork)
            {
                return _unitOfWork.GetRepository<Function>().All().ToList();
            }
        }

        public List<Function> GetFunctionsByModule(string moduleId)
        {
            using (_unitOfWork)
            {
                return _unitOfWork.GetRepository<Function>().Filter(x => x.ModuleId == moduleId).ToList();
            }
        }
    }
}
