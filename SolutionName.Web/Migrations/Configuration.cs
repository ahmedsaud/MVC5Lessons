namespace SolutionName.Web.Migrations
{
    using SolutionName.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SolutionName.DataLayer.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SolutionName.DataLayer.SalesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            
            /*
            context.SalesOrders.AddOrUpdate(
                so => so.CustomerName,
                new SalesOrder { CustomerName = "Adam", PONumber = "9876", SalesOrderItems =
                    {
                        new SalesOrderItem{ProductId = 1, Quantity = 10, UnitPrice = 1.23m },
                        new SalesOrderItem{ProductId = 2, Quantity = 7, UnitPrice = 14.57m },
                        new SalesOrderItem{ProductId = 3, Quantity = 3, UnitPrice = 15.00m }
                    }
                },
                new SalesOrder { CustomerName = "Michael"},
                new SalesOrder { CustomerName = "David", PONumber = "Acme 9"}
                );
        */
        }
    }
}
