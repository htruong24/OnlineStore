using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IMenuService
    {
        Menu GetMenu(int? menuId);
        void UpdateMenu(Menu menu);
        void CreateMenu(Menu menu);
        void DeleteMenu(int? menuId);
        List<Menu> GetMenus();
    }
}
