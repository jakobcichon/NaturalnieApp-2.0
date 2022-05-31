using MySql.Data;
using MySql.Data.EntityFramework;
using NaturalnieApp2.Services.DTOs;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;


namespace NaturalnieApp.Database
{
    // Code-Based Configuration and Dependency resolution
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ShopContext : DbContext
    {
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<ProductChangelogDTO> ProductsChangelog { get; set; }
        public DbSet<SaleDTO> Sales { get; set; }
        public DbSet<StockDTO> Stock { get; set; }
        public DbSet<StockHistoryDTO> StockHistory { get; set; }
        public DbSet<SupplierDTO> Suppliers { get; set; }
        public DbSet<ManufacturerDTO> Manufacturers { get; set; }
        public DbSet<TaxDTO> Tax { get; set; }
        public DbSet<ElzabCommunicationDTO> ElzabCommunication { get; set; }
        public DbSet<InventoryDTO> Inventory { get; set; }
        public DbSet<InventoryHistoryDTO> InventoryHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configures one-to-many relationship
            modelBuilder.Entity<ProductDTO>()
                .HasRequired(p => p.Tax)
                .WithMany(x => x.Products)
                .HasForeignKey(p => p.TaxId);

            modelBuilder.Entity<ProductDTO>()
                .HasRequired(p => p.Supplier)
                .WithMany(x => x.Products)
                .HasForeignKey(p => p.SupplierId);

            modelBuilder.Entity<ProductDTO>()
                .HasRequired(p => p.Manufacturer)
                .WithMany(x => x.Products)
                .HasForeignKey(p => p.ManufacturerId);
        }

        public ShopContext()
            : base("shop")
        {
            CommonSettings();
        }

        public ShopContext(string connectionString)
            : base(connectionString)
        {
            CommonSettings();
        }
        // Constructor to use on a DbConnection that is already opened
        public ShopContext(DbConnection existingConnection, bool contextOwnsConnection)
          : base(existingConnection, contextOwnsConnection)
        {
            CommonSettings();
        }

        private void CommonSettings()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
