namespace LocalStore
{
    class Startup
    {
        static void Main(string[] args)
        {     

            Product milk = new Product("Milk", "Vereq", "Mlqko ot krava", 2.00M);

            Product Chees = new Product("Sirene", "Bojenci", "Krave sirene", 12.00M);

            Product yellowCheese = new Product("Kashkaval", "Bojenci", "Kashkaval..", 15.00M);

            ProductsContext context = new ProductsContext();

            context.Products.Add(milk);
            context.Products.Add(Chees);
            context.Products.Add(yellowCheese);

            context.SaveChanges();
        }
    }
}
