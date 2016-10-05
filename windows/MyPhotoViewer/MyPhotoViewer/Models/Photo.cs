using System;

namespace MyPhotoViewer.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Path { get; set; }

        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}