using System;

namespace MyPhotoViewer.Core
{
    public interface IPhoto
    {
        int Id { get; }
        int PhotoAlbumId { get; }
        IPhotoAlbum PhotoAlbum { get; }
        string Title { get; }
        DateTime? CreationDate { get; }

        Image GetImage();
    }

    public interface IUpdatablePhoto
    {
        string Title { get; set; }
        int PhotoAlbumId { get; set; }
        DateTime? CreationDate { get; set; }

        void Update();
    }
}
