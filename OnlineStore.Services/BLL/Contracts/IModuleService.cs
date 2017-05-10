﻿using System.Collections.Generic;
using OnlineStore.Data.Entities;
using OnlineStore.Services.Models;

namespace OnlineStore.Services.BLL.Contracts
{
    public interface IModuleService
    {
        Module GetModule(string moduleId);
        void UpdateModule(Module module);
        void CreateModule(Module module);
        void DeleteModule(string moduleId);
        List<Module> GetModules();
    }
}