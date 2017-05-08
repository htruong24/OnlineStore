using System.Collections.Generic;
using OnlineStore.Data.Entities;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IAutomaticValueService
    {
        AutomaticValue GetAutomaticValue(string objectName);
        string GetNextId(string objectName);
        List<AutomaticValue> GetAutomaticValues();
        void UpdateAutomaticValue(AutomaticValue user);
    }
}
