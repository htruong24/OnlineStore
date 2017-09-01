using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface ICountryService
    {
        Country GetCountry(int? country);
        void UpdateCountry(Country menu);
        void CreateCountry(Country menu);
        void DeleteCountry(int? country);
        List<Country> GetCountrys();
    }
}
