using SolutionName.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionName.DataLayer
{
    public class SalesContext: DbContext
    {
        public SalesContext()
            : base("DefaultConnection")
        {
        }
        
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        /*
        protected override void OnModelCreating(DbModelBuilder _modelBuilder)
        {
            _modelBuilder.Configurations.Add(new SalesOrderConfiguration());
            _modelBuilder.Configurations.Add(new SalesOrderItemConfiguration());
            _modelBuilder.Configurations.Add(new ProductConfiguration());
        }*/
    }
}