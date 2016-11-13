using MyPhotoViewer.Core;

namespace MyPhotoViewer.ViewModels
{
    public interface IPhotoAlbumThumbnail
    {
        int PhotoAlbumId { get; }
        int CoverPhotoId { get; }
        string Name { get; }
        string Description { get; }
        IPlace Place { get; }
        IDateTimePeriod Period { get;}
    }

    public class PhotoAlbumThumbnail : IPhotoAlbumThumbnail
    {
        public int PhotoAlbumId { get; set; }
        public int CoverPhotoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IPlace Place { get; set; }
        public IDateTimePeriod Period { get; set; }
    }
}