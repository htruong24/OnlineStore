using System.Collections.Generic;
using System.Linq;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Services.BLL.Contracts;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Services
{
    public class ModuleService: IModuleService
    {
        public SortingPagingInfo Pagination;
        private readonly UnitOfWork _unitOfWork;

        public ModuleService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Module GetModule(string moduleId)
        {
            using (_unitOfWork)
            {
                var module = _unitOfWork.GetRepository<Data.Entities.Module>().GetById(moduleId);
                return module;
            }
        }

        public void UpdateModule(Module module)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Data.Entities.Module>().Update(module);
                _unitOfWork.Save();
            }
        }

        public void CreateModule(Module module)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Module>().Create(module);
                _unitOfWork.Save();
            }
        }

        public void DeleteModule(string moduleId)
        {
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Module>().Delete(moduleId);
                _unitOfWork.Save();
            }
        }

        public List<Module> GetModules()
        {
            using (_unitOfWork)
            {
                return _unitOfWork.GetRepository<Module>().All().ToList();
            }
        }
    }
}
