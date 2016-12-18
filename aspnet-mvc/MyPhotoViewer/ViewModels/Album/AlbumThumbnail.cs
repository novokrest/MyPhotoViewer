using MyPhotoViewer.Core;

namespace MyPhotoViewer.ViewModels.Album
{
    public interface IAlbumThumbnail
    {
        int AlbumId { get; }
        int CoverPhotoId { get; }
        string Name { get; }
        string Description { get; }
        IPlace Place { get; }
        IDateTimePeriod Period { get;}
        int PhotosCount { get; }
    }

    public class AlbumThumbnail : IAlbumThumbnail
    {
        public int AlbumId { get; set; }
        public int CoverPhotoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IPlace Place { get; set; }
        public IDateTimePeriod Period { get; set; }
        public int PhotosCount { get; set; }
    }
}