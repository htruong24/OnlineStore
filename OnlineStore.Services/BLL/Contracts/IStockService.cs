using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IStockService
    {
        Stock GetStock(int? stockId);
        void UpdateStock(Stock stock);
        void CreateStock(Stock stock);
        void DeleteStock(int? stockId);
        List<Stock> GetStocks();
    }
}
