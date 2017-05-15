using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IUnitService
    {
        Unit GetUnit(int? unitId);
        void UpdateUnit(Unit unit);
        void CreateUnit(Unit unit);
        void DeleteUnit(int? unitId);
        List<Unit> GetUnits();
    }
}
