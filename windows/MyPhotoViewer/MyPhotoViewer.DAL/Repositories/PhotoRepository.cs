using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.DAL
{
    internal sealed class PhotoRepository : IPhotoRepository
    {
        private readonly PhotosContext _photoAlbumContext;

        public PhotoRepository(PhotosContext photoAlbumContext)
        {
            _photoAlbumContext = photoAlbumContext;
        }

        public Photo GetPhotoById(int photoId)
        {
            return _photoAlbumContext.Photos.Find(photoId);
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return _photoAlbumContext.Photos.Include("Place");
        }

        public void InsertPhoto(Photo photo)
        {
            _photoAlbumContext.Photos.Add(photo);
        }

        public void UpdatePhoto(Photo photo)
        {
            throw new NotImplementedException();
        }

        public void DeletePhoto(int photo)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
