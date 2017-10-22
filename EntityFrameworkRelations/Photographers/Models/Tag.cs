using System.Collections.Generic;

namespace Photographers.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Albums = new HashSet<Album>();
        }

        public int Id { get; set; }
        public string Label { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
