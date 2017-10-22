using StudentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentSystem
{
    class Startup
    {
        static void Main(string[] args)
        {
            var context = new StudentsContext();

            foreach (var students in context.Students
                                            .OrderByDescending(x=>x.Courses.Sum(z=>z.Price))
                                            .ThenByDescending(x=>x.Courses.Count)
                                            .ThenBy(n=>n.Name))
            {
                Console.WriteLine($"{students.Name} - Courses: {students.Courses.Count}, Total Price: {students.Courses.Sum(x=>x.Price)}, Avarage Price: {students.Courses.Average(p=>p.Price)}");
            }
        }
        private static void CoursesWtihMoreTHan5Resources(StudentsContext context)
        {
            var courses = context.Courses.Where(c => c.Resources.Count() > 5)
                                         .OrderByDescending(c => c.Resources.Count())
                                         .ThenByDescending(c => c.StartDate)
                                         .ToList();

            foreach (var c in courses)
            {
                Console.WriteLine($"{c.Name} - {c.Resources.Count()}");
            }
        }
        private static void AddSomeData(StudentsContext context)
        {
            Course course = new Course()
            {
                Name = "C#",
                Description = "Learning to programing with C#",
                StartDate = new DateTime(2016, 2, 9),
                EndDate = new DateTime(2016, 5, 5),
                Price = 20,
                HomeworkSubmissions = new List<Homework>()
                {
                    new Homework()
                    {
                        Content = "Hello C#",
                        ContentType = ContentType.PDF,
                        SumbmissionDate = new DateTime(2016, 2, 15),
                        StudentId = new Student()
                        {
                            Name = "Stella",
                            RegisteredOn = new DateTime(2016, 1, 6),
                            PhoneNumber = "0894651278"
                        }
                    },
                    new Homework()
                    {
                        Content = "CRUD Operations",
                        ContentType = ContentType.Application,
                        SumbmissionDate = new DateTime(2016, 2, 18),
                        StudentId = new Student()
                        {
                            Name = "Tosho",
                            RegisteredOn = new DateTime(2016, 1, 5),
                            PhoneNumber = "0892654861"
                        }
                    }
                },
                Resources = new List<Resource>()
                {
                    new Resource()
                    {
                        Name = "Introduction in C#",
                        ReourceType = TypeOfResource.Presentation,
                        Url = "SoftUni.bg/trainings/c#"
                    },
                    new Resource()
                    {
                        Name = "Basic CRUD Operations",
                        ReourceType = TypeOfResource.Video,
                        Url = "SoftUni.bg/trainings/c#"
                    },
                    new Resource()
                    {
                        Name = "Loops",
                        ReourceType = TypeOfResource.Presentation,
                        Url = "SoftUni.bg/trainings/c#"
                    },
                    new Resource()
                    {
                        Name = "Advanced Loops",
                        ReourceType = TypeOfResource.Presentation,
                        Url = "SoftUni.bg/trainings/c#"
                    },
                    new Resource()
                    {
                        Name = "Entity Framework",
                        ReourceType = TypeOfResource.Other,
                        Url = "SoftUni.bg/trainings/c#"
                    },
                    new Resource()
                    {
                        Name = "Arrays and Lists",
                        ReourceType = TypeOfResource.Other,
                        Url = "SoftUni.bg/trainings/c#"
                    }
                }
            };

            context.Courses.Add(course);
            context.SaveChanges();
        }
        private static void ListAllCoursesWithTheirResources(StudentsContext context)
        {
            var courses = context.Courses.OrderBy(c => c.StartDate).ThenByDescending(c => c.EndDate).ToList();
            foreach (var c in courses)
            {
                Console.WriteLine($"{c.Name} - {c.Description}");

                foreach (var r in c.Resources)
                {
                    Console.WriteLine($"{r.Name} - {r.ReourceType} URL: {r.Url}");
                }
            }
        }
        private static void ListAllStudentsHomeworks(StudentsContext context)
        {
            var students = context.Students.ToList();

            foreach (var s in students)
            {
                Console.WriteLine($"{s.Name}: ");
                foreach (var h in s.Homewors)
                {
                    Console.WriteLine($"    {h.Content} {h.ContentType}");
                }
            }
        }
    }
}
