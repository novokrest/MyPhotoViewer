using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.DAL
{
    internal sealed class PhotoRepository : IPhotoRepository
    {
        private readonly PhotoAlbumContext _photoAlbumContext;

        public PhotoRepository(PhotoAlbumContext photoAlbumContext)
        {
            _photoAlbumContext = photoAlbumContext;
        }

        public Photo GetPhotoById(int photoId)
        {
            return _photoAlbumContext.Photos
                                     .Include("Place")
                                     .Where(photo => photo.PhotoId == photoId)
                                     .Single();
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return _photoAlbumContext.Photos.Include("Place");
        }

        public void InsertPhoto(Photo photo)
        {
            throw new NotImplementedException();
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
