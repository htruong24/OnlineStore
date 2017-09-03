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
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ProductPhoto> ProductPhotoes { get; set; }
        public virtual DbSet<RecommendProduct> RecommendProducts { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }
    }
}
