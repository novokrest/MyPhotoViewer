using MyPhotoViewer.Models;

namespace MyPhotoViewer.ViewModels
{
    public interface IPhotoCollectionThumbnail
    {
        int Id { get; }
        string Name { get; }
        string Image { get; }
        string Description { get; }
        Place Place { get; }
        DateTimePeriod Period { get;}
    }

    public class PhotoCollectionThumbnail : IPhotoCollectionThumbnail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Place Place { get; set; }
        public DateTimePeriod Period { get; set; }
    }
}