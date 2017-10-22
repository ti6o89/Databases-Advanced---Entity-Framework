using System;
using System.ComponentModel.DataAnnotations;

namespace CreateUser.Models
{
    public class Users
    {
        private string username;
        public int Id { get; set; }

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                if (value.Length < 4 || value.Length > 30)
                {
                    throw new ArgumentOutOfRangeException(
                        "The username should be between 4 and 30 symbols.");
                    this.username = value;
                }
            }
        }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,50}$")]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(8388608)]
        public byte[] ProfilePicture { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }
    }
}
