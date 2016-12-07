using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using System.Collections.Generic;

namespace MyPhotoViewer.DAL
{
    public interface IPhotoAlbumRepository
    {
        IEnumerable<IPhotoAlbum> GetPhotoAlbums();
        IPhotoAlbum GetPhotoAlbumById(int photoAlbumId);

        void AddPhotoAlbum(PhotoAlbumEntity photoAlbum);
        void UpdatePhotoAlbum(PhotoAlbumEntity photoAlbum);
        void SavePhotoAlbum(PhotoAlbumEntity photoAlbum);
        void Save();
    }
}
