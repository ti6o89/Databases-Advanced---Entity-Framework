namespace Sales
{
    class Program
    {
        static void Main(string[] args)
        {
            SalesContext context = new SalesContext();
            context.Database.Initialize(true);
        }
    }
}
