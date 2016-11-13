using MyPhotoViewer.Core;
using System.Collections.Generic;


namespace MyPhotoViewer.ViewModels
{
    public class PhotoAlbumWithPhotosViewModel
    {
        public IPhotoAlbum PhotoAlbum { get; set; }
        public IEnumerable<IPhoto> Photos { get; set; }
    }
}