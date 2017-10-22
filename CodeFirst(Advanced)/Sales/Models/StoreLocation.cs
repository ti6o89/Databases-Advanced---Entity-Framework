using System.Collections.Generic;

namespace Sales
{
    public class StoreLocation
    {
        public int Id { get; set; }

        public string LocationName { get; set; }

        HashSet<Sale> SalesInStore;
    }
}
