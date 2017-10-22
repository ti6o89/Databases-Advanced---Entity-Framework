using Gringotts.Data;
using System;
using System.Linq;

namespace Gringotts
{
    class Startup
    {
        static void Main(string[] args)
        {
            var context = new GringottsContext();
            
        }
        private static void DepositsSumForOllivanderFamily(GringottsContext context)
        {
            var groups = context.WizzardDeposits
                .Where(m => m.MagicWandCreator == "Ollivander family")
                .GroupBy(x => x.DepositGroup)
                .Select(x => new { groupName = x.Key, sum = x.Sum(z => z.DepositAmount) });

            foreach (var g in groups)
            {
                Console.WriteLine($"{g.groupName} - {g.sum}");
            }
        }
        private static void DepositFilter(GringottsContext context)
        {
            var groups = context.WizzardDeposits
                .Where(m => m.MagicWandCreator == "Ollivander family")
                .GroupBy(x => x.DepositGroup)
                .Where(a => a.Sum(z => z.DepositAmount) < 150000)
                .Select(x => new { groupName = x.Key, sum = x.Sum(z => z.DepositAmount) })
                .OrderByDescending(a => a.sum);

            foreach (var g in groups)
            {
                Console.WriteLine($"{g.groupName} - {g.sum}");
            }
        }
    }
}
