using System;

namespace CreateUser
{
    class Startup
    {
        static void Main(string[] args)
        {
            var ctx = new UserContext();
            ctx.Database.Initialize(true);

            Models.Users user1 = new Models.Users()
            {
                Username = "user1",
                Password = "iferb^4Sdftrh",
                Email = "user1@mail.vf",
                RegisteredOn = new DateTime(2015, 10, 11),
                Age = 10,
                IsDeleted = false
            };
            ctx.Users.Add(user1);
            ctx.SaveChanges();
        }
    }
}
