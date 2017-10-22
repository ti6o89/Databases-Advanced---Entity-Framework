namespace CarDealer.App
{
    using System;
    using Data;
    using Models;
    using System.Xml.Linq;
    using System.Linq;
    using System.Collections.Generic;

    public class Application
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            context.Database.Initialize(true);
            
        }
        //Query 6 – Sales with Applied Discount
        private static void SalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                            .Select(a => new
                            {
                                carMake = a.Car.Make,
                                carModel = a.Car.Model,
                                travelledDistance = a.Car.TravelledDistance,
                                name = a.Customer.Name,
                                discount = a.Discount,
                                price = a.Car.Parts.Sum(p => p.Price),
                                priceWithDiscount = (1 - a.Discount) * a.Car.Parts.Sum(p => p.Price)
                            });

            XDocument salesDocument = new XDocument();
            XElement salesXml = new XElement("sales");

            foreach (var s in sales)
            {
                XElement sale = new XElement("sale");

                XElement car = new XElement("car");
                car.SetAttributeValue("make", s.carMake);
                car.SetAttributeValue("model", s.carModel);
                car.SetAttributeValue("travelled-distance", s.travelledDistance);

                XElement customer = new XElement("customer-name");
                customer.Value = s.name;

                XElement discount = new XElement("discount");
                discount.Value = s.discount.ToString();

                XElement price = new XElement("price");
                price.Value = s.price.ToString();

                XElement priceWithDiscount = new XElement("price-with-disocount");
                priceWithDiscount.Value = s.priceWithDiscount.ToString();

                sale.Add(car);
                sale.Add(customer);
                sale.Add(discount);
                sale.Add(price);
                sale.Add(priceWithDiscount);

                salesXml.Add(sale);
            }
            salesDocument.Add(salesXml);
            salesDocument.Save("../../sales-discounts.xml");
        }
        //Query 5 – Total Sales by Customer
        private static void TotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                            .Where(c => c.Sales.Count() > 0)
                            .Select(a => new
                            {
                                Name = a.Name,
                                boughtCars = a.Sales.Count(),
                                totalSpentMoney = a.Sales.Sum(x => x.Car.Parts.Sum(y => y.Price))
                            })
                            .OrderByDescending(a => a.totalSpentMoney)
                            .ThenByDescending(a => a.boughtCars);

            XDocument customerDocument = new XDocument();
            XElement customersXml = new XElement("customers");

            foreach (var c in customers)
            {
                XElement customer = new XElement("customer");
                customer.SetAttributeValue("full-name", c.Name);
                customer.SetAttributeValue("bought-cars", c.boughtCars);
                customer.SetAttributeValue("spent-money", c.totalSpentMoney);

                customersXml.Add(customer);
            }
            customerDocument.Add(customersXml);
            customerDocument.Save("../../customers-total-sales.xml");
        }
        //Query 4 – Cars with Their List of Parts
        private static void CarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                            .Select(a => new
                            {
                                Make = a.Make,
                                Model = a.Model,
                                TravelledDistance = a.TravelledDistance,
                                Parts = a.Parts.Select(x => new
                                {
                                    Name = x.Name,
                                    Price = x.Price
                                })
                            });

            XDocument carsDocument = new XDocument();
            XElement carsXml = new XElement("cars");

            foreach (var c in cars)
            {
                XElement car = new XElement("car");
                car.SetAttributeValue("make", c.Make);
                car.SetAttributeValue("model", c.Model);
                car.SetAttributeValue("travelled-distance", c.TravelledDistance);

                XElement parts = new XElement("parts");

                foreach (var p in c.Parts)
                {
                    XElement part = new XElement("part");
                    part.SetAttributeValue("name", p.Name);
                    part.SetAttributeValue("price", p.Price);

                    parts.Add(part);
                }
                car.Add(parts);
                carsXml.Add(car);
            }
            carsDocument.Add(carsXml);
            carsDocument.Save("../../cars-and-parts.xml");
        }
        //Query 3 – Local Suppliers
        private static void LocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                            .Where(s => s.IsImporter == false)
                            .Select(a => new
                            {
                                id = a.Id,
                                name = a.Name,
                                numberOfParts = a.Parts.Count()
                            });

            XDocument suppliersDocument = new XDocument();
            XElement supplierXml = new XElement("suppliers");

            foreach (var s in suppliers)
            {
                XElement supplier = new XElement("supplier");
                supplier.SetAttributeValue("id", s.id);
                supplier.SetAttributeValue("name", s.name);
                supplier.SetAttributeValue("parts-count", s.numberOfParts);

                supplierXml.Add(supplier);
            }
            suppliersDocument.Add(supplierXml);
            suppliersDocument.Save("../../local-suppliers.xml");
        }
        //Query 2 – Cars from make Ferrari
        private static void CarsFormMakeFerrari(CarDealerContext context)
        {
            var cars = context.Cars
                            .Where(m => m.Make == "Ferrari")
                            .OrderBy(m => m.Model)
                            .ThenByDescending(d => d.TravelledDistance);

            XDocument carsDocument = new XDocument();
            XElement carsXml = new XElement("cars");

            foreach (var c in cars)
            {
                XElement car = new XElement("car");
                car.SetAttributeValue("id", c.Id);
                car.SetAttributeValue("model", c.Model);
                car.SetAttributeValue("travelled-distance", c.TravelledDistance);

                carsXml.Add(car);
            }
            carsDocument.Add(carsXml);
            carsDocument.Save("../../ferrari-cars.xml");
        }
        //Query 1 – Cars
        private static void Cars(CarDealerContext context)
        {
            var cars = context.Cars
                            .Where(d => d.TravelledDistance > 2000000)
                            .OrderBy(m => m.Make)
                            .ThenBy(m => m.Model);

            XDocument carsDocument = new XDocument();
            XElement carsXml = new XElement("cars");

            foreach (var c in cars)
            {
                XElement car = new XElement("car");

                XElement make = new XElement("make");
                make.Value = c.Make;

                XElement model = new XElement("model");
                model.Value = c.Model;

                XElement travelledDistance = new XElement("travelled-distance");
                travelledDistance.Value = c.TravelledDistance.ToString();

                car.Add(make);
                car.Add(make);
                car.Add(model);
                car.Add(travelledDistance);

                carsXml.Add(car);
            }
            carsDocument.Add(carsXml);
            carsDocument.Save("../../cars.xml");
        }

        private static void ImportSales(CarDealerContext context)
        {
            Random rnd = new Random();
            decimal[] discounts = new[] { 0m, 0.05m, 0.1m, 0.15m, 0.2m, 0.3m, 0.4m, 0.5m };
            int salesToMake = rnd.Next(8, 18);
            int carsCount = context.Cars.Count();
            int customersCount = context.Customers.Count();
            List<Sale> sales = new List<Sale>();

            for (int i = 0; i < salesToMake; i++)
            {
                int discountIndex = rnd.Next(0, 7);
                int carId = rnd.Next(1, carsCount);
                int customerId = rnd.Next(1, customersCount);

                Sale sale = new Sale();
                sale.CarId = carId;
                sale.CustomerId = customerId;
                sale.Discount = discounts[discountIndex];
                sales.Add(sale);
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        private static void ImportCustomers(CarDealerContext context)
        {
            XDocument customersDocument = XDocument.Load("../../Import/customers.xml");
            var customersXml = customersDocument.Root.Elements();

            /*<customer name="Marcelle Griego">
                 <birth-date>1990-10-04T00:00:00</birth-date>
                 <is-young-driver>true</is-young-driver>
            </customer>*/

            foreach (var customerXml in customersXml)
            {
                string name = customerXml.Attribute("name").Value;
                DateTime birthDate = DateTime.Parse(customerXml.Element("birth-date").Value);
                bool isYoungDriver = bool.Parse(customerXml.Element("is-young-driver").Value);

                Customer customer = new Customer()
                {
                    Name = name,
                    BirthDate = birthDate,
                    IsYoungDriver = isYoungDriver
                };
                context.Customers.Add(customer);
            }
            context.SaveChanges();
        }

        private static void ImportCars(CarDealerContext context)
        {
            XDocument carsDocument = XDocument.Load("../../Import/cars.xml");
            var carsXml = carsDocument.Root.Elements();
            Random rnd = new Random();

            /*<car>
                <make>Opel</make>
                <model>Astra</model>
                <travelled-distance>9223372036854775807</travelled-distance>
            </car>*/

            foreach (var carElement in carsXml)
            {
                string make = carElement.Element("make").Value;
                string model = carElement.Element("model").Value;
                long travelledDistance = long.Parse(carElement.Element("travelled-distance").Value);

                int partsNumber = rnd.Next(10, 21);
                List<Part> parts = new List<Part>();
                for (int i = 0; i < partsNumber; i++)
                {
                    int partId = rnd.Next(1, context.Parts.Count() + 1);
                    parts.Add(context.Parts.Find(partId));
                }

                Car car = new Car()
                {
                    Make = make,
                    Model = model,
                    TravelledDistance = travelledDistance,
                    Parts = parts
                };
                context.Cars.Add(car);
            }
            context.SaveChanges();
        }

        private static void ImportParts(CarDealerContext context)
        {
            XDocument partsDocument = XDocument.Load("../../Import/parts.xml");
            var partsXml = partsDocument.Root.Elements();

            // <part name="Cowl screen" price="1500.34" quantity="10" />

            Random rnd = new Random();
            foreach (var p in partsXml)
            {
                string name = p.Attribute("name").Value;
                decimal price = decimal.Parse(p.Attribute("price").Value);
                int quantity = int.Parse(p.Attribute("quantity").Value);
                int supplierId = rnd.Next(1, context.Suppliers.Count() + 1);

                Part part = new Part()
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Supplier = context.Suppliers.Find(supplierId)
                };

                context.Parts.Add(part);
            }
            context.SaveChanges();
        }

        private static void ImportSuppliers(CarDealerContext context)
        {
            XDocument suppliersDocument = XDocument.Load("../../Import/suppliers.xml");
            var suppliers = suppliersDocument.Root;

            //<supplier name="3M Company" is-importer="true" />

            foreach (var s in suppliers.Elements())
            {
                string name = s.Attribute("name")?.Value;
                bool isImporter = bool.Parse(s.Attribute("is-importer").Value);

                Supplier supplier = new Supplier()
                {
                    Name = name,
                    IsImporter = isImporter
                };
                context.Suppliers.Add(supplier);
            }
            context.SaveChanges();
        }
    }
}
