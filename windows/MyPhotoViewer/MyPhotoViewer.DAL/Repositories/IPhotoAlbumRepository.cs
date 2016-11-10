using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.DAL
{
    public interface IPhotoAlbumRepository
    {
        IEnumerable<PhotoAlbum> GetPhotoAlbums();
        PhotoAlbum GetPhotoAlbumById(int photoAlbumId);
        void AddPhotoAlbum(PhotoAlbum photoAlbum);

        void SavePhotoAlbum(PhotoAlbum photoAlbum);
        void Save();
    }
}
