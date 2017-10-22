using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Introduction_to_Entity_Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            var project = context.Projects.Find(2);

           // foreach (Employee emp in project.Employees)
           // {
           //     emp.Projects.Remove(project);
           // }
           //
           // context.Projects.Remove(project);
           // context.SaveChanges();

            var projects = context.Projects.Take(10);

            foreach (Project p in projects)
            {
                Console.WriteLine(p.Name);
            }
        }
        private static void FirstLetter(GringottsContext context)
        {
            var letter = context.WizzardDeposits
                                .Where(wd => wd.DepositGroup == "Troll Chest")
                                .Select(wd => wd.FirstName)
                                .ToList()
                                .Select(fn => fn[0])
                                .Distinct()
                                .OrderBy(c => c);

            foreach (char l in letter)
            {
                Console.WriteLine(l);
            }
        }
        private static void FindEmployeesByFirstNameStarting(SoftUniContext context)
        {
            var employyes = context.Employees.Where(e => e.FirstName.ToLower().StartsWith("sa"));

            foreach (Employee e in employyes)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F4})");
            }
        }
        private static void IncreaseSalaries(SoftUniContext context)
        {
            var employees = context.Employees.Where(e => e.Department.Name == "Engineering" ||
                                                         e.Department.Name == "Tool Design" ||
                                                         e.Department.Name == "Marketing" ||
                                                         e.Department.Name == "Information Services");

            foreach (Employee e in employees)
            {
                e.Salary *= 1.12M;
            }
            context.SaveChanges();

            foreach (Employee e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:F6})");
            }
        }
        private static void DeparmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments.Where(e => e.Employees.Count > 5).OrderBy(e => e.Employees.Count);

            foreach (var dep in departments)
            {
                Console.WriteLine($"{dep.Name} {dep.Menager.FirstName}");

                foreach (var e in dep.Employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} {e.JobTitle}");
                }
            }
        }
        private static void EmployeeWithId147(SoftUniContext context)
        {
            var emp = context.Employees.FirstOrDefault(e => e.EmployeeID == 147);


            Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.JobTitle}");

            foreach (var p in emp.Projects.OrderBy(s => s.Name))
            {
                Console.WriteLine($"{p.Name}");
            }
        }
        private static void AdressesByTownName(SoftUniContext context)
        {
            List<Address> adresses = context.Addresses.OrderByDescending(a => a.Employees.Count)
                                                      .ThenBy(a => a.Town.Name)
                                                      .Take(10)
                                                      .ToList();

            foreach (Address a in adresses)
            {
                Console.WriteLine($"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees");
            }

        }
        private static void EmployeessInPeriod(SoftUniContext context)
        {
            var emp = context.Employees.Where(e => e.Projects.Any(p => 2001 <= p.StartDate.Year && p.StartDate.Year <= 2003))
                                       .Take(30)
                                       .ToList();

            foreach (var e in emp)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} {e.Manager.FirstName}");

                foreach (var p in e.Projects)
                {
                    Console.WriteLine($"--{p.Name} {p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)} {p.EndDate:M'/'d'/'yyyy h:mm:ss tt}");
                }
            }
        }
        private static void AddingNewAdress(SoftUniContext context)
        {
            Address adress = new Address();
            adress.AddressText = "Vitoshka 15";
            adress.TownID = 4;

            Employee emp = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            emp.Address = adress;
            context.SaveChanges();

            var adresses = context.Employees.OrderByDescending(e => e.AddressID).Take(10).ToList();

            foreach (Employee e in adresses)
            {
                Console.WriteLine(e.Address.AddressText);
            }
        }
        private static void EmployeesFromSeattle(SoftUniContext context)
        {
            List<Employee> employeeFromSeattle = context.Employees.Where(e => e.Department.Name == "Research and Development")
                                                                  .OrderBy(e => e.Salary)
                                                                  .ThenByDescending(e => e.FirstName)
                                                                  .ToList();
            foreach (var e in employeeFromSeattle)
            {
                Console.WriteLine($@"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:f2}");
            }
        }
        private static void EmployeesWithSalaryOver50000(SoftUniContext context)
        {
            List<Employee> employees = context.Employees.Where(e => e.Salary > 50000).ToList();

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName}");
            }
        }
        private static void EmployeesFullInformation(SoftUniContext context)
        {
            List<Employee> employees = context.Employees.ToList();

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:F4}");
            }
        }
    }
}
