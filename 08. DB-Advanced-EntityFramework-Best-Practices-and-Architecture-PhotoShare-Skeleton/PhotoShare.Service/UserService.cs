using PhotoShare.Client;
using PhotoShare.Models;
using System;
using System.Linq;

namespace PhotoShare.Service
{
    public class UserService
    {
        public void Add(string username, string password, string email)
        {
            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };

            using (PhotoShareContext context = new PhotoShareContext())
            {

                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void Remove(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {

                User user = context.Users.SingleOrDefault(u => u.Username == username);
                if (user != null)
                {
                    user.IsDeleted = true;
                    context.SaveChanges();
                }

            }
        }

        public bool IsTaken(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Users.Any(u => u.Username == username);
            }
        }

        public User GetUserByUsername(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Users.SingleOrDefault(u => u.Username == username);
            }
        }
        public void UpdateUser(User u)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {

                User userInBase = context.Users.Find(u.Id);
                var entry = context.Entry(userInBase);

                entry.CurrentValues.SetValues(u);
                entry.OriginalValues.SetValues(userInBase);

                context.SaveChanges();
            }
        }
    }
}
