using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Infrastructure;

namespace OnlineStore.Data.Interfaces
{
   public interface IUnitOfWork
    {
       IRepository<T> GetRepository<T>() where T : class;
       void Save();
    }
}
