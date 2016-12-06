using MyPhotoViewer.Core;
using System.Collections.Generic;


namespace MyPhotoViewer.ViewModels
{
    public class PhotoAlbumWithPhotosViewModel
    {
        public IPhotoAlbum PhotoAlbum { get; set; }
        public IEnumerable<IPhoto> Photos { get; set; }
    }

    public class PhotoAlbumViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IDateTimePeriod Period { get; set; }
        public IPlace Place { get; set; }
        public int PhotosCount { get; set; }
    }
}