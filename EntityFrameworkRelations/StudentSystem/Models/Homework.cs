using System;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{
    public enum ContentType
    {
        Application,
        PDF,
        Zip
    }
    public class Homework
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public ContentType ContentType { get; set; }
        [Required]
        public DateTime SumbmissionDate { get; set; }
        [Required]
        public Course CourseId { get; set; }
        [Required]
        public Student StudentId { get; set; }
    }
}
