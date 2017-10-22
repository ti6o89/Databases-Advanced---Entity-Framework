using System.Collections.Generic;

namespace Hospital.Models
{
    public class Medicament
    {
        public Medicament()
        {
            this.Patients = new List<Patient>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Patient> Patients { get; set; }
    }
}
