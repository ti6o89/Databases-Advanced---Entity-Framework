using Newtonsoft.Json;
using ProductsShop.Data;
using ProductsShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductsShop.Client
{
    class Startup
    {
        static void Main(string[] args)
        {
            ProductsShopContext context = new ProductsShopContext();
            CategoriesByProductsCount(context);
        }

        private static void UsersAndProducts(ProductsShopContext context)
        {
            var users = context.Users
                            .Include("SoldProducts")
                            .Where(sp => sp.SoldProducts.Count() > 1)
                            .OrderByDescending(p => p.SoldProducts.Count())
                            .ThenBy(u => u.LastName)
                            .Select(a => new
                            {
                                firstName = a.FirstName,
                                lastName = a.LastName,
                                age = a.Age,
                                soldProducts = a.SoldProducts.Select(p => new
                                {
                                    name = p.Name,
                                    price = p.Price
                                })
                            });

            string usersJson = JsonConvert.SerializeObject(new
            {
                usersCount = users.Count(),
                users = users
            }, Formatting.Indented);
            File.WriteAllText("../../users-and-products.json", usersJson);
        }

        private static void CategoriesByProductsCount(ProductsShopContext context)
        {
            var categories = context.Categories
                            .OrderBy(c => c.Name)
                            .Select(c => new
                            {
                                Category = c.Name,
                                ProductsCount = c.Products.Count,
                                AveragePrice = c.Products.Average(p => p.Price).ToString(),
                                TotalRevenue = c.Products.Sum(p => p.Price).ToString()
                            });

            string categoryJson = JsonConvert.SerializeObject(categories, Formatting.Indented);
            File.WriteAllText("../../categories-by-products.json", categoryJson);
        }

        private static void SuccessfullySoldProducts(ProductsShopContext context)
        {
            var users = context.Users
                            .Include("SoldProducts")
                            .Where(u => u.SoldProducts.Count >= 1)
                            .Select(u => new
                            {
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                SoldProducts = u.SoldProducts.Where(p => p.Buyer != null).Select(p => new
                                {
                                    Name = p.Name,
                                    Price = p.Price,
                                    BuyerFirstName = p.Buyer.FirstName,
                                    BuyerLastName = p.Buyer.LastName
                                })
                            })
                            .OrderBy(u => u.LastName)
                            .ThenBy(u => u.FirstName);


            string usersJson = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText("../../users-Sold-products.json", usersJson);
        }

        private static void ProductsInRange(ProductsShopContext context)
        {
            var products = context.Products
                            .Include("Seller")
                            .Where(p => p.Price >= 500 && p.Price <= 1000)
                            .OrderBy(p => p.Price)
                            .Select(a => new
                            {
                                ProductName = a.Name,
                                Price = a.Price,
                                Seller = a.Seller.FirstName + " " + a.Seller.LastName
                            });

            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductName} {p.Price} {p.Seller}");
            }

            string productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText("../../products-in-range.json", productsJson);
        }

        private static void ImportCategories(ProductsShopContext context)
        {
            string categoriesJson = File.ReadAllText("../../Import/categories.json");

            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson);

            int number = 0;
            int productsCount = context.Products.Count();
            foreach (Category c in categories)
            {
                int categoryProductsCount = number % 3;
                for (int i = 0; i < categoryProductsCount; i++)
                {
                    c.Products.Add(context.Products.Find((number % productsCount) + 1));
                }
                number++;
            }
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ImportProducts(ProductsShopContext context)
        {
            string productsJson = File.ReadAllText("../../Import/products.json");

            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(productsJson);

            int number = 0;
            int usersCount = context.Users.Count();
            foreach (var p in products)
            {
                p.SellerId = (number % usersCount) + 1;
                if (number % 3 != 0)
                {
                    p.BuyerId = (number * 2 % usersCount) + 1;
                }

                number++;
            }
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void ImportUsers(ProductsShopContext context)
        {
            string usersJson = File.ReadAllText("../../Import/users.json");

            List<User> users = JsonConvert.DeserializeObject<List<User>>(usersJson);
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
