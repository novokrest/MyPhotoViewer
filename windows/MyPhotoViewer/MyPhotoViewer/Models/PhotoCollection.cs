using System.Collections.Generic;

namespace MyPhotoViewer.Models
{
    public interface IPhotoCollection
    {
        string Name { get; }
        string Description { get; }
        Place Place { get; }
        DateTimePeriod Period { get; }
        List<Photo> Photos { get; }
    }

    public class PhotoCollection : IPhotoCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }

        public DateTimePeriod Period { get; set; }

        public List<Photo> Photos { get; set; }
    }
}