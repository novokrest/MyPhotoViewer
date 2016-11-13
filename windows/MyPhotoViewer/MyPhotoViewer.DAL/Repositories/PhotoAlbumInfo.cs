using MyPhotoViewer.Core;
using System.Collections.Generic;


namespace MyPhotoViewer.DAL.Repositories
{
    class PhotoAlbumInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Image> PhotoImages { get; set; }
    }
}
