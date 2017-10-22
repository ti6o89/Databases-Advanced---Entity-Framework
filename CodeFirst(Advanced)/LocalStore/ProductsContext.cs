namespace LocalStore
{
    using System.Data.Entity;

    public class ProductsContext : DbContext
    {
        // Your context has been configured to use a 'LocalStoreContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LocalStore.LocalStoreContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'LocalStoreContext' 
        // connection string in the application configuration file.
        public ProductsContext()
            : base("name=LocalStoreContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Product> Products { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}