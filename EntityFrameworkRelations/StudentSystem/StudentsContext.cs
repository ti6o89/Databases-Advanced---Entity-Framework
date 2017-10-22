namespace StudentSystem
{
    using Migrations;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class StudentsContext : DbContext
    {
        // Your context has been configured to use a 'StudentsContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'StudentSystem.StudentsContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'StudentsContext' 
        // connection string in the application configuration file.
        public class StudentSystemInitializer : CreateDatabaseIfNotExists<StudentsContext>
        {
            protected override void Seed(StudentsContext context)
            {
                Course course =
                        new Course()
                        {
                            Name = "Java",
                            Description = "Programing language",
                            StartDate = new DateTime(2016, 5, 20),
                            EndDate = DateTime.Now,
                            Price = 250,
                            HomeworkSubmissions = new List<Homework>()
                    {
                        new Homework()
                        {
                            Content = "Java basic",
                            ContentType = ContentType.PDF,
                            SumbmissionDate = new DateTime(2017, 7, 3),
                            StudentId = new Student()
                            {
                                Name = "Ivan",
                                RegisteredOn = new DateTime(2016, 2, 6),
                                PhoneNumber = "0889975535"
                            }
                        },
                        new Homework()
                        {
                            Content = "Java advanced",
                            ContentType = ContentType.PDF,
                            SumbmissionDate = new DateTime(2017, 7, 3),
                            StudentId = new Student()
                            {
                                Name = "Pesho",
                                RegisteredOn = new DateTime(2016, 2, 6),
                                PhoneNumber = "0888574521"
                            }
                        }
                    },
                            Students = new List<Student>()
                    {
                        new Student()
                        {
                            Name = "Gosho",
                            RegisteredOn = DateTime.Today,
                            PhoneNumber = "0894574524"
                        },
                        new Student()
                        {
                            Name = "Drago",
                            RegisteredOn = DateTime.Today,
                            PhoneNumber = "0898774414"
                        },new Student()
                        {
                            Name = "Mitko",
                            RegisteredOn = DateTime.Today,
                            PhoneNumber = "0897571562"
                        }
                    },
                            Resources = new List<Resource>()
                    {
                        new Resource()
                        {
                            Name = "Programing - tips and tricks",
                            ReourceType = TypeOfResource.Presentation,
                            Url = "programing/basic/tips"
                        },
                        new Resource()
                        {
                            Name = "Programing for dummies",
                            ReourceType = TypeOfResource.Video,
                            Url = "programing/basic/tips"
                        }
                    }
                };
                context.Courses.Add(course);
                context.SaveChanges();
            }
        }

        public StudentsContext()
            : base("name=StudentsContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentsContext, Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Student> Students { get; set; }
         public virtual DbSet<Resource> Resources { get; set; }
         public virtual DbSet<Homework> Homeworks { get; set; }
         public virtual DbSet<Course> Courses { get; set; }
         public virtual DbSet<License> Licenses { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}