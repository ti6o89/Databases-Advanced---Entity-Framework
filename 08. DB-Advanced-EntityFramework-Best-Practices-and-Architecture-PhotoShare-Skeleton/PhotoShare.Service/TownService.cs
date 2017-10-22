
using PhotoShare.Client;
using PhotoShare.Models;
using System.Linq;

namespace PhotoShare.Service
{
    public class TownService
    {
        public void AddTown(string name, string countryName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Town t = new Town();
                t.Name = name;
                t.Country = countryName;

                context.Towns.Add(t);
                context.SaveChanges();
            }
        }

        public bool IsTownExisting(string townName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Towns.Any(t => t.Name == townName);
            }
        }
        public Town GetByTownName(string townName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Towns.SingleOrDefault(t => t.Name == townName);
            }
        }
    }
}
