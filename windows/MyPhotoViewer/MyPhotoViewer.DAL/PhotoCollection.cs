using System.Collections.Generic;

namespace MyPhotoViewer.DAL
{
    public class PhotoCollection
    {
        public int PhotoCollectionId { get; set; }
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimePeriod Period { get; set; }

        public virtual Place Place { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}