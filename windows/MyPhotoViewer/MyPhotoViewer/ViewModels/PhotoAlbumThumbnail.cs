using MyPhotoViewer.DAL;
using MyPhotoViewer.Models;

namespace MyPhotoViewer.ViewModels
{
    public interface IPhotoAlbumThumbnail
    {
        int PhotoAlbumId { get; }
        string Name { get; }
        string Image { get; }
        string Description { get; }
        Place Place { get; }
        DateTimePeriod Period { get;}
    }

    public class PhotoAlbumThumbnail : IPhotoAlbumThumbnail
    {
        public int PhotoAlbumId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Place Place { get; set; }
        public DateTimePeriod Period { get; set; }
    }
}