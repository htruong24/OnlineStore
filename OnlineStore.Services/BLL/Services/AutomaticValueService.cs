using System.Collections.Generic;
using System.Linq;
using OnlineStore.Common;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;
using OnlineStore.Services.BLL.Contracts;

namespace OnlineStore.Services.BLL.Services
{
    public class AutomaticValueService : IAutomaticValueService
    {
        private readonly UnitOfWork _unitOfWork;

        public AutomaticValueService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public AutomaticValue GetAutomaticValue(string objectName)
        {
            var automaticValue = _unitOfWork.GetRepository<Data.Entities.AutomaticValue>().GetById(objectName);
            return automaticValue;
        }

        public string GetNextId(string objectName)
        {
            var automaticValue = _unitOfWork.GetRepository<Data.Entities.AutomaticValue>().GetById(objectName);

            var newId = automaticValue.Prefix + automaticValue.Character;

            var currentIdNumber = string.IsNullOrEmpty(automaticValue.LastValue)
                ? 0
                : int.Parse(
                    automaticValue.LastValue.Substring(automaticValue.Prefix.Length +
                                                                 automaticValue.Character.Length));

            currentIdNumber = currentIdNumber + 1;
            var numberLength = newId.Length;

            for (var i = 1; i <= automaticValue.Length - currentIdNumber.ToString().Length - numberLength; i++)
            {
                newId += "0";
            }
            newId += currentIdNumber;

            return newId;
        }

        public List<AutomaticValue> GetAutomaticValues()
        {
            using (_unitOfWork)
            {
                return _unitOfWork.GetRepository<AutomaticValue>().All().ToList();
            }
        }

        public void UpdateAutomaticValue(AutomaticValue automaticValue)
        {
            _unitOfWork.GetRepository<Data.Entities.AutomaticValue>().Update(automaticValue);
            _unitOfWork.Save();
        }
    }
}
