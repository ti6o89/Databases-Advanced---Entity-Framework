﻿using System.Collections.Generic;

namespace CarDealer.Models
{
    public class Part
    {
        public Part()
        {
            this.Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Supplier Supplier { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
