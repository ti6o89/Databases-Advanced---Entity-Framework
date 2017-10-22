namespace Sales.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Sales.SalesContext";
        }

        protected override void Seed(SalesContext context)
        {
            context.Products.AddOrUpdate(new Product() { Name = "Car", Description = "Vehicle" });
            context.Products.AddOrUpdate(new Product() { Name = "Truck", Description = "Vehicle" });
            context.Products.AddOrUpdate(new Product() { Name = "Motorcycle", Description = "Vehicle" });
            context.Products.AddOrUpdate(new Product() { Name = "Bus", Description = "Vehicle" });
            context.Products.AddOrUpdate(new Product() { Name = "Ski", Description = "Ski" });

            context.Customers.AddOrUpdate(new Customer() { FirstName = "Petar", LastName = "Petrov" });
            context.Customers.AddOrUpdate(new Customer() { FirstName = "Ivan", LastName = "Ivanov" });
            context.Customers.AddOrUpdate(new Customer() { FirstName = "Dimitar", LastName = "Georgiev" });
            context.Customers.AddOrUpdate(new Customer() { FirstName = "Georgi", LastName = "Dimitrov" });
            context.Customers.AddOrUpdate(new Customer() { FirstName = "Alex", LastName = "Todorov" });
         
             //base.Seed(context);
        }
    }
}
