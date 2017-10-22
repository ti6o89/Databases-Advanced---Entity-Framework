
namespace Photographers
{
    using Models;
    using System.Collections.Generic;
    public class Album
    {
        public Album()
        {
            this.Pictures = new HashSet<Picture>();
            this.Tags = new HashSet<Tag>();
            this.Owners = new HashSet<Photographer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public bool IsPublic { get; set; }
        public int OwnerId { get; set; }

        public virtual ICollection<Photographer> Owners { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
