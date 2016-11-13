using System;

namespace MyPhotoViewer.Core
{
    public interface IPhoto
    {
        int Id { get; }
        int PhotoAlbumId { get; }
        string Title { get; }
        DateTime? CreationDate { get; }

        Image GetImage();
    }
}
