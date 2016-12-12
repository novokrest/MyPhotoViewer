using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using System.Collections.Generic;

namespace MyPhotoViewer.DAL
{
    public interface IAlbumRepository
    {
        IEnumerable<IAlbum> LoadAlbums();
        IAlbum GetAlbumById(int albumId);
        void RemoveAlbumById(int albumId);

        void AddAlbum(AlbumEntity album);
        void UpdateAlbum(AlbumEntity album);
        void SaveAlbum(AlbumEntity album);
        void Save();
    }
}
