using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface ISupplierService
    {
        Supplier GetSupplier(int? supplierId);
        void UpdateSupplier(Supplier supplier);
        void CreateSupplier(Supplier supplier);
        void DeleteSupplier(int? supplierId);
        List<Supplier> GetSuppliers();
    }
}
