using System.Collections.Generic;

namespace MyPhotoViewer.DAL
{
    public class Place
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}