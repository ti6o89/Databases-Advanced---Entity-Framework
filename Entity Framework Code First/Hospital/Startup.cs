using System;

namespace Hospital
{
    class Startup
    {
        static void Main(string[] args)
        {
            var ctx = new HospitalContext();

            ctx.Database.Initialize(true);

            Models.Patient p = new Models.Patient()
            {
                FirstName = "Ivan",
                LastName = "Georgiev",
                Address = "Pleven",
                DateOfBirth = new DateTime(1990, 8, 20),
                Email = "IvanG@abv.bg",
                HasInsurance = true
            };

            Models.Diagnose d = new Models.Diagnose()
            {
                Name = "grip",
                Comment = "Zimna nastinka"
            };


            ctx.Patients.Add(p);
            p.Diagnoses.Add(d);
            
        }
    }
}
