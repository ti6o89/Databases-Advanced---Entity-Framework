using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{
    public class Student
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
            this.Homewors = new List<Homework>();
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime RegisteredOn { get; set; }
        public DateTime? BirthDay { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public  virtual ICollection<Homework> Homewors { get; set; }
    }
}
