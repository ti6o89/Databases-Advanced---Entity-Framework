namespace ProductsShop
{
    using System.Linq;
    using System.Xml.Linq;
    using Data;
    using Model;
    using System;
    using System.Collections.Generic;

    public class Application
    {
        public static void Main(string[] args)
        {
            ProductShopContext context = new ProductShopContext();
            
        }
        //Query 4 - Users and Products
        private static void UsersAndProducts(ProductShopContext context)
        {
            var users = context.Users
                            .Where(p => p.ProductsSold.Count() > 0)
                            .OrderByDescending(sp => sp.ProductsSold.Count())
                            .Select(a => new
                            {
                                firstName = a.FirstName,
                                lastName = a.LastName,
                                age = a.Age,
                                productsCount = a.ProductsSold.Count(),
                                products = a.ProductsSold.Select(x => new
                                {
                                    name = x.Name,
                                    price = x.Price
                                })
                            });

            XDocument usersDocument = new XDocument();
            XElement usersXml = new XElement("users");
            usersXml.SetAttributeValue("count", context.Users.Count());

            foreach (var u in users)
            {
                XElement user = new XElement("user");
                user.SetAttributeValue("first-name", u.firstName);
                user.SetAttributeValue("last-name", u.lastName);
                user.SetAttributeValue("age", u.age);

                XElement soldProducts = new XElement("sold-products");
                soldProducts.SetAttributeValue("count", u.productsCount);

                foreach (var products in u.products)
                {
                    XElement product = new XElement("product");
                    product.SetAttributeValue("name", products.name);
                    product.SetAttributeValue("price", products.price);

                    soldProducts.Add(product);
                }
                user.Add(soldProducts);

                usersXml.Add(user);
            }
            usersDocument.Add(usersXml);
            usersDocument.Save("../../users-and-products.xml");
        }
        //Query 3 - Categories By Products Count
        private static void CategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories.Select(a => new
            {
                name = a.Name,
                numberOfProducts = a.Products.Count(),
                averagePrice = a.Products.Average(x => x.Price),
                totalRevenue = a.Products.Sum(x => x.Price)
            })
                        .OrderByDescending(a => a.numberOfProducts);

            XDocument categoriesDocument = new XDocument();
            XElement categoriesXml = new XElement("categories");

            foreach (var c in categories)
            {
                XElement category = new XElement("category");
                category.SetAttributeValue("name", c.name);

                XElement productsCount = new XElement("products-count");
                productsCount.Value = c.numberOfProducts.ToString();

                XElement avaragePrice = new XElement("avarage-price");
                avaragePrice.Value = c.averagePrice.ToString();

                XElement totalRevenue = new XElement("total-revenue");
                totalRevenue.Value = c.totalRevenue.ToString();

                category.Add(productsCount);
                category.Add(avaragePrice);
                category.Add(totalRevenue);

                categoriesXml.Add(category);
            }
            categoriesDocument.Add(categoriesXml);
            categoriesDocument.Save("../../categories-by-products.xml");
        }
        //Query 2 - Sold Products
        private static void SoldProducts(ProductShopContext context)
        {
            var users = context.Users
                            .Where(s => s.ProductsSold.Count() > 0)
                            .Select(a => new
                            {
                                firstName = a.FirstName,
                                lastName = a.LastName,
                                soldProducts = a.ProductsSold.Select(x => new
                                {
                                    productName = x.Name,
                                    price = x.Price
                                })
                            })
                            .OrderBy(a => a.lastName)
                            .ThenBy(a => a.firstName);

            XDocument usersDocument = new XDocument();
            XElement usersXml = new XElement("users");

            foreach (var u in users)
            {
                XElement user = new XElement("user");
                user.SetAttributeValue("first-name", u.firstName);
                user.SetAttributeValue("last-name", u.lastName);

                XElement sodlProducts = new XElement("sold-products");

                foreach (var sp in u.soldProducts)
                {
                    XElement product = new XElement("product");

                    XElement name = new XElement("name");
                    name.Value = sp.productName;

                    XElement price = new XElement("price");
                    price.Value = sp.price.ToString();

                    product.Add(name);
                    product.Add(price);

                    sodlProducts.Add(product);
                }
                user.Add(sodlProducts);

                usersXml.Add(user);
            }

            usersDocument.Add(usersXml);
            usersDocument.Save("../../users-sold-products.xml");
        }
        //Query 1 - Products In Range
        private static void ProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                            .Where(p => p.Price >= 1000 && p.Price <= 2000)
                            .Where(b => b.Buyer != null)
                            .Select(a => new
                            {
                                name = a.Name,
                                price = a.Price,
                                buyer = a.Buyer.FirstName + " " + a.Buyer.LastName
                            })
                            .OrderBy(p => p.price);

            XDocument productsDocument = new XDocument();
            XElement productsXml = new XElement("products");

            foreach (var product in products)
            {
                XElement productXml = new XElement("product");
                productXml.SetAttributeValue("name", product.name);
                productXml.SetAttributeValue("price", product.price);
                productXml.SetAttributeValue("buyer", product.buyer);

                productsXml.Add(productXml);
            }

            productsDocument.Add(productsXml);
            productsDocument.Save("../../products-in-range.xml");
        }

        private static void ImportCategories(ProductShopContext context)
        {
            XDocument xmlCategoies = XDocument.Load("../../Import/categories.xml");
            var categories = xmlCategoies.Root.Elements();

            Random rnd = new Random();
            foreach (var c in categories)
            {
                string name = c.Element("name").Value;
                Category category = new Category()
                {
                    Name = name
                };
                context.Categories.Add(category);
            }
            context.SaveChanges();

            List<Product> products = context.Products.ToList();
            foreach (var p in products)
            {
                List<Category> productCategories = new List<Category>();
                int categoryCount = rnd.Next(1, 5);
                for (int i = 0; i < categoryCount; i++)
                {
                    int categoryId = rnd.Next(1, context.Categories.Count() + 1);
                    productCategories.Add(context.Categories.Find(categoryId));
                }
                p.Categories = productCategories;
                context.SaveChanges();
            };
        }

        private static void ImportProducts(ProductShopContext context)
        {
            //<name>Care One Hemorrhoidal</name>
            XDocument xmlProducts = XDocument.Load("../../Import/products.xml");
            var products = xmlProducts.Root.Elements();

            int number = 1;
            Random rnd = new Random();
            foreach (var p in products)
            {
                int sellerId = rnd.Next(1, context.Users.Count() + 1);
                int buyerId = rnd.Next(1, context.Users.Count() + 1);

                string name = p.Element("name").Value;
                decimal price = decimal.Parse(p.Element("price").Value);
                if (number % 4 == 0)
                {
                    Product product = new Product()
                    {
                        Name = name,
                        Price = price,
                        Seller = context.Users.Find(sellerId)
                    };
                    context.Products.Add(product);
                }
                else
                {
                    Product product = new Product()
                    {
                        Name = name,
                        Price = price,
                        Seller = context.Users.Find(sellerId),
                        Buyer = context.Users.Find(buyerId)
                    };
                    context.Products.Add(product);
               }
                number++;                
            }
            context.SaveChanges();
        }

        private static void ImportUsers(ProductShopContext context)
        {
            //<user first-name="Eugene" last-name="Stewart" age="65"/>
            XDocument xmlUsers = XDocument.Load("../../Import/users.xml");
            var users = xmlUsers.Root.Elements();

            foreach (var u in users)
            {
                string firstName = u.Attribute("first-name")?.Value;
                string lastName = u.Attribute("last-name").Value;
                int? age = int.Parse(u.Attribute("age")?.Value ?? "0");


                User user = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
