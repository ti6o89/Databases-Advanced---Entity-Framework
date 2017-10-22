namespace Photographers
{
    using Migrations;
    using Models;
    using System.Data.Entity;

    public class PhotographersContext : DbContext
    {
        // Your context has been configured to use a 'PhotographersContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Photographers.PhotographersContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PhotographersContext' 
        // connection string in the application configuration file.
        public PhotographersContext()
            : base("name=PhotographersContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotographersContext, Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Photographer> Photographers { get; set; }
         public virtual DbSet<Album> Albums { get; set; }
         public virtual DbSet<Picture> Pictures { get; set; }
         public virtual DbSet<Tag> Tags { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}