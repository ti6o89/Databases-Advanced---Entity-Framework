
namespace BookShopSystem
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var context = new BookShopContext();

        }
        private static void StoredProcedure(BookShopContext context)
        {
            Console.Write("Please Ener Author name: ");
            string[] names = Console.ReadLine().Split();
            int count = context.Database
                .SqlQuery<int>("EXEC dbo.usp_GetBooksCountByAuthor {0}, {1}", names[0], names[1]).First();
            Console.WriteLine($"{names[0]} {names[1]} has written {count} books");
        }
        private static void RemoveBooks(BookShopContext context)
        {
            var books = context.Books.Where(x => x.Copies < 4200).ToList();
            Console.WriteLine($"{books.Count} books were deleted");
            context.Books.RemoveRange(books);
            context.SaveChanges();
        }
        private static void MostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Where(x => x.Books.Count > 35)
                .OrderByDescending(x => x.Books.Count)
                .Select(a => new
                {
                    categoryName = a.Name,
                    bookCount = a.Books.Count,
                    books = a.Books
                                                                                                .OrderByDescending(x => x.ReleaseDate)
                                                                                                .ThenBy(x => x.Title)
                                                                                                .Select(x => new { title = x.Title, date = x.ReleaseDate.Value.Year })
                                                                                                .Take(3)
                })
                .ToList();

            foreach (var c in categories)
            {
                Console.WriteLine($"--{c.categoryName}: {c.bookCount} books");
                foreach (var book in c.books)
                {
                    Console.WriteLine($"{book.title} ({book.date})");
                }
            }
        }
        private static void FindProfit(BookShopContext context)
        {
            var categories = context.Categories
                .Select(a => new { categoryName = a.Name, Profit = a.Books.Sum(b => b.Copies * b.Price) })
                .OrderByDescending(p => p.Profit)
                .ThenBy(c => c.categoryName)
                .ToList();

            foreach (var c in categories)
            {
                Console.WriteLine($"{c.categoryName} - ${c.Profit}");
            }
        }
        private static void BookTitlesByCategory(BookShopContext context)
        {
            string[] categoryType = Console.ReadLine()
                .Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.ToLower())
                .ToArray();
            foreach (var book in context.Books)
            {
                if (book.Categories.Any(c => categoryType.Contains(c.Name.ToLower())))
                {
                    Console.WriteLine(book.Title);
                }
            }
        }
        private static void TotalBookCopies(BookShopContext context)
        {
            var books = context.Books
                .GroupBy(a => a.Author)
                .Select(a => new
                {
                    author = a.Key,
                    Copies = a.Sum(x => x.Copies)
                })
                .OrderByDescending(c => c.Copies)
                .ToList();

            foreach (var book in books)
            {
                Console.WriteLine($"{book.author.FirstName} {book.author.LastName} - {book.Copies}");
            }
        }
        private static void CountBooks(BookShopContext context)
        {
            var number = int.Parse(Console.ReadLine());
            var books = context.Books.Where(t => t.Title.Length > number).ToList();
            Console.WriteLine(books.Count);
        }
        private static void BookTitleSearch(BookShopContext context)
        {
            string input = Console.ReadLine().ToLower();

            var books = context.Books
                .Where(a => a.Author.LastName.ToLower().StartsWith(input))
                .OrderBy(i => i.Id)
                .Select(x => new
                {
                    x.Title,
                    fName = x.Author.FirstName,
                    lName = x.Author.LastName
                })
                .ToList();

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} ({book.fName} {book.lName})");
            }

        }
        private static void BooksSearch()
        {
            var context = new BookShopContext();

            string input = Console.ReadLine().ToLower();
            var query = "SELECT * FROM Books WHERE Title LIKE @param";
            var param = new SqlParameter("@param", "%" + input + "%");
            var books = context.Database.SqlQuery<Book>(query, param);

            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }
        private static void AuthorsSearch()
        {
            var context = new BookShopContext();

            string input = Console.ReadLine();
            var query = "SELECT * FROM Authors WHERE FirstName LIKE @firstNameParam";
            var firstNameParam = new SqlParameter("@firstNameParam", "%" + input);
            var authors = context.Database.SqlQuery<Author>(query, firstNameParam);

            foreach (var a in authors)
            {
                Console.WriteLine($"{a.FirstName} {a.LastName}");
            }
        }
        private static void BooksReleasedBeforeDate()
        {
            var context = new BookShopContext();
            var date = DateTime.Parse(Console.ReadLine());
            var books = context.Books.Where(y => y.ReleaseDate < date);

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - {book.Edition} - {book.Price}");
            }
        }
        private static void NotReleasedBooks()
        {
            var input = int.Parse(Console.ReadLine());

            var context = new BookShopContext();
            var books = context.Books.Where(r => r.ReleaseDate.Value.Year != input);

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title}");
            }

        }
        private static void BooksByPrice()
        {
            var context = new BookShopContext();
            var books = context.Books.Where(p => p.Price < 5 || p.Price > 40)
                                     .OrderBy(i => i.Id);

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - ${book.Price}");
            }
        }
        private static void GoldenBooks()
        {
            var context = new BookShopContext();
            var books = context.Books.Where(e => e.Edition.ToString()
                                                          .ToLower()
                                                          .Equals("gold"))
                                     .Where(c => c.Copies < 5000)
                                     .OrderBy(i => i.Id)
                                     .Select(t => t.Title);

            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
        private static void BooksTitlesByAgeRestriction()
        {
            var input = Console.ReadLine().ToLower();

            var context = new BookShopContext();
            var books = context.Books
                    .Where(b => b.AgeRestriction.ToString()
                                                 .ToLower()
                                                 .Equals(input))
                    .Select(c => c.Title);

            Console.WriteLine(string.Join("\n", books));
        }
    }
}
