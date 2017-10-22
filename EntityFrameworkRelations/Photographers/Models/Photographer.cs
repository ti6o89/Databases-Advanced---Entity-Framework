using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photographers
{
    public class Photographer
    {
        public Photographer()
        {
            this.Albums = new HashSet<Album>();
        }

        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime Birthdate { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
