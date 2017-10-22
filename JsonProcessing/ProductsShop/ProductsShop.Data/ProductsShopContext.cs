namespace ProductsShop.Data
{
    using Models;
    using System.Data.Entity;

    public class ProductsShopContext : DbContext
    {
        public ProductsShopContext()
            : base("name=ProductsShopContext")
        {
        }

         public virtual DbSet<User> Users { get; set; }
         public virtual DbSet<Product> Products { get; set; }
         public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.BoughtProducts).WithOptional(p => p.Buyer);

            modelBuilder.Entity<User>().HasMany(p => p.SoldProducts).WithRequired(b => b.Seller);

            //modelBuilder.Entity<UserFriend>().HasRequired(uf => uf.Users).WithMany(u => u.Friends)
            //    .HasForeignKey(u => u.UserId).WillCascadeOnDelete(false);
            //
            //modelBuilder.Entity<UserFriend>().HasRequired(uf => uf.Users).WithMany(u => u.Friends)
            //    .HasForeignKey(u => u.UserId).WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().HasMany(f => f.Friends).WithMany();/*.Map(mc =>
            {
                mc.MapLeftKey("UserdId");
                mc.MapRightKey("FriendId");
                mc.ToTable("UserFriends");
            });*/
            base.OnModelCreating(modelBuilder); 
        }
    }
}