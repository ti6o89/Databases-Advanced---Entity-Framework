using SoftUni.Data;
using SoftUni.ViewModels;
using System;
using System.Linq;

namespace SoftUni
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();
            
        }
        private static void CallAStoredProcedure(SoftUniContext context)
        {
            Console.Write("Enter employee's names: ");
            string[] names = Console.ReadLine().Split();

            var projects = context.Database
                .SqlQuery<ProjectViewModel>("EXEC dbo.udp_FindProjectsByEmployeeName {0}, {1}", names[0], names[1]);

            foreach (ProjectViewModel project in projects)
            {
                Console.WriteLine($"{project.Name} - {project.Description}, {project.StartDate}");
            }
        }
        private static void EmployeesMaxiumumSalaries(SoftUniContext context)
        {
            var departments = context.Departments
                .Select(x => new { name = x.Name, salary = x.Employees.Max(a => a.Salary) })
                .Where(a => a.salary < 30000 || a.salary > 70000)
                .ToList();

            foreach (var d in departments)
            {
                Console.WriteLine($"{d.name} - {d.salary:F2}");
            }
        }
    }
}
