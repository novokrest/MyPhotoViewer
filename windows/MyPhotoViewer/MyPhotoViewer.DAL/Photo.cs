using System;

namespace MyPhotoViewer.DAL
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public int PhotoCollectionId { get; set; }
        public int PlaceId { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Path { get; set; }

        public virtual Place Place { get; set; }
        public virtual PhotoCollection PhotoCollection { get; set; }
    }
}