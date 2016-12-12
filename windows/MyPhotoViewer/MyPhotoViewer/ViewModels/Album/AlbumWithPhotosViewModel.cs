using MyPhotoViewer.Core;
using System.Collections.Generic;

namespace MyPhotoViewer.ViewModels.Album
{
    public class AlbumWithPhotosViewModel : BaseAlbumViewModel
    {
        public IEnumerable<IPhoto> Photos { get; set; }
    }
}