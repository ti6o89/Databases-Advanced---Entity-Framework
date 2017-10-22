namespace StudentSystem.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.StudentsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StudentsContext context)
        {
            context.Courses.AddOrUpdate(c => c.Name,
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
                });
        }
    }
}
