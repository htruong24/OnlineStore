using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface ICityService
    {
        City GetCity(int? cityId);
        void UpdateCity(City city);
        void CreateCity(City city);
        void DeleteCity(int? cityId);
        List<City> GetCities();
    }
}
