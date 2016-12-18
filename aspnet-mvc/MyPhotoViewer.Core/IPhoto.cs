using System;

namespace MyPhotoViewer.Core
{
    public interface IPhoto
    {
        int Id { get; }
        int AlbumId { get; }
        IAlbum PhotoAlbum { get; }
        string Title { get; }
        DateTime? CreationDate { get; }

        Image GetImage();
    }

    public interface IUpdatablePhoto
    {
        string Title { get; set; }
        int AlbumId { get; set; }
        DateTime? CreationDate { get; set; }

        void Update();
    }
}
