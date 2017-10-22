using CarDealer.Data;
using CarDealer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarDealer.Client
{
    class Startup
    {
        static void Main(string[] args)
        {
            var context = new CarDealerContext();
            
        }

        private static void SalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales.Select(a => new
            {
                car = new
                {
                    a.Car.Make,
                    a.Car.Model,
                    a.Car.TravelledDistance
                },
                customerName = a.Customer.Name,
                a.Discount,
                price = a.Car.Parts.Sum(p => p.Price),
                priceWithDiscount = (1 - a.Discount) * a.Car.Parts.Sum(p => p.Price)
            });

            string salesJson = JsonConvert.SerializeObject(sales, Formatting.Indented);
            File.WriteAllText("../../sales-dicounts.json", salesJson);
        }

        private static void TotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                            .Where(s => s.Sales.Count() > 0)
                            .Select(a => new
                            {
                                fullName = a.Name,
                                boughtCars = a.Sales.Count(),
                                spentMoney = a.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price))
                            })
                            .OrderByDescending(s => s.spentMoney)
                            .ThenByDescending(bc => bc.boughtCars);

            string customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
            File.WriteAllText("../../customers-total-sales.json", customersJson);
        }

        private static void CarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars.Select(a => new
            {
                Make = a.Make,
                Model = a.Model,
                TravelledDistance = a.TravelledDistance,
                parts = a.Parts.Select(x => new
                {
                    Name = x.Name,
                    Price = x.Price
                })
            });
            string carsJson = JsonConvert.SerializeObject(new { car = cars }, Formatting.Indented);
            File.WriteAllText("../../cars-and-parts.json", carsJson);
        }

        private static void LocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                            .Where(p => p.IsImporter == false)
                            .Select(a => new
                            {
                                Id = a.Id,
                                Name = a.Name,
                                PartsCount = a.Parts.Count()
                            });
            string suppliersJson = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
            File.WriteAllText("../../local-suppliers.json", suppliersJson);
        }

        private static void CarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                            .Where(m => m.Make == "Toyota")
                            .OrderBy(m => m.Model)
                            .ThenByDescending(d => d.TravelledDistance);

            string carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);
            File.WriteAllText("../../toyota-cars.json", carsJson);
        }

        private static void OrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                            .OrderBy(bd => bd.BirthDate)
                            .ThenBy(y => y.IsYoungDriver);

            string customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
            File.WriteAllText("../../order-customers.json", customersJson);
        }

        private static void ImportSales(CarDealerContext context)
        {
            Random rnd = new Random();
            decimal[] discounts = new[] { 0m, 0.05m, 0.1m, 0.15m, 0.2m, 0.3m, 0.4m, 0.5m };
            int carsCount = context.Cars.Count();
            int customersCount = context.Customers.Count();
            List<Sale> sales = new List<Sale>();

            int salesCount = rnd.Next(10, 26);

            for (int i = 0; i < salesCount; i++)
            {
                int discountIndex = rnd.Next(0, 7);
                int carId = rnd.Next(1, carsCount);
                int customerId = rnd.Next(1, customersCount);

                Sale sale = new Sale();
                sale.Car_Id = carId;
                sale.Customer_Id = customerId;
                sale.Discount = discounts[discountIndex];
                sales.Add(sale);
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        private static void ImportCustomers(CarDealerContext context)
        {
            string customersJson = File.ReadAllText("../../Import/customers.json");
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(customersJson);
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void ImportCars(CarDealerContext context)
        {
            string carsJson = File.ReadAllText("../../Import/cars.json");
            List<Car> cars = JsonConvert.DeserializeObject<List<Car>>(carsJson);

            List<Part> parts = context.Parts.ToList();
            Random rnd = new Random();

            foreach (var car in cars)
            {
                int partsCount = rnd.Next(10, 21);
                for (int i = 0; i < partsCount; i++)
                {
                    int partId = rnd.Next(1, parts.Count() + 1);
                    car.Parts.Add(context.Parts.Find(partId));
                }
            }
            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void ImportParts(CarDealerContext context)
        {
            string partsJson = File.ReadAllText("../../Import/parts.json");
            List<Part> parts = JsonConvert.DeserializeObject<List<Part>>(partsJson);

            Random rnd = new Random();
            List<Supplier> suppliers = context.Suppliers.ToList();


            foreach (var p in parts)
            {
                int supplierId = rnd.Next(1, suppliers.Count - 1);
                p.Supplier = context.Suppliers.Find(supplierId);
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();
        }

        private static void ImportSuppliers(CarDealerContext context)
        {
            string suppliersJson = File.ReadAllText("../../Import/suppliers.json");
            List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(suppliersJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }
    }
}
