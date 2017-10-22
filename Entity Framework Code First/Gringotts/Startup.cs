using System;

namespace Gringotts
{
    class Startup
    {
        static void Main(string[] args)
        {
            var ctx = new GringottsContext();
            ctx.Database.Initialize(true);

            Models.WizardDeposits dumbledore = new Models.WizardDeposits()
            {
                FirstName = "Albus",
                LastName = "Dumbledore",
                Age = 150,
                MagicWandCreator = "Antioch Peverell",
                MagicWandSize = 15,
                DepositStartDate = new DateTime(2016, 10, 20),
                DepositExpirationDate = new DateTime(2016, 10, 20),
                DepositAmount = 20000.24m,
                DepositCharge = 0.2,
                IsDepositExpired = false
            };

            ctx.Deposits.Add(dumbledore);
            ctx.SaveChanges();

            
        }
    }
}
