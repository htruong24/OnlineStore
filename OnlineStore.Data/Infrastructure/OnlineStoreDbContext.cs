using OnlineStore.Common;
using System.Data.Entity;
using OnlineStore.Data.Entities;

namespace OnlineStore.Data.Infrastructure
{
    public class OnlineStoreDbContext : DbContext
    {
        public OnlineStoreDbContext()
            : base(CommonConstants.CONNECTION_STRING)
        {
            Database.SetInitializer<OnlineStoreDbContext>(null);
        }

        public virtual DbSet<AutomaticValue> AutomaticValues { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}