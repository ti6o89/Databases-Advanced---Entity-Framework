using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Patient
    {
        public Patient()
        {
            this.Visitations = new List<Visitation>();
            this.Diagnoses = new List<Diagnose>();
            this.Medicaments = new List<Medicament>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[] Picture { get; set; }

        public bool HasInsurance { get; set; }

        public virtual List<Diagnose> Diagnoses { get; set; }
        public virtual List<Medicament> Medicaments { get; set; }
        public virtual List<Visitation> Visitations { get; set; }
    }
}
