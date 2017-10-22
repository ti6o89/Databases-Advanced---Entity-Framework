using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Visitation
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(200)]
        public string Comment { get; set; }
    }
}
