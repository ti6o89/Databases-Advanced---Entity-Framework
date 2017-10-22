using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{
    public enum TypeOfResource
    {
        Video,
        Presentation,
        Document,
        Other
    }

    public class Resource
    {
        public Resource()
        {
            this.Licenses = new HashSet<License>();
        }
        
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public TypeOfResource ReourceType { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int CourseId { get; set; }

        ICollection<License> Licenses { get; set; }
    }
}
