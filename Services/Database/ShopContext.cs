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

        public ShopContext()
            : base("shop")
        {

        }

        public ShopContext(string connectionString)
            : base(connectionString)
        {

        }
        // Constructor to use on a DbConnection that is already opened
        public ShopContext(DbConnection existingConnection, bool contextOwnsConnection)
          : base(existingConnection, contextOwnsConnection)
        {

        }

    }

}
