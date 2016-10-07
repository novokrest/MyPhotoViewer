using MyPhotoViewer.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.DAL
{
    internal class PhotoCollectionRepository : IPhotoCollectionRepository, IDisposable
    {
        private readonly PhotoAlbumContext _photoAlbumContext;

        public PhotoCollectionRepository(PhotoAlbumContext photoAlbumContext)
        {
            Verifiers.ArgNullVerify(photoAlbumContext, nameof(photoAlbumContext));
            _photoAlbumContext = photoAlbumContext;
        }

        public PhotoCollection GetPhotoCollectionById(int photoCollectionId)
        {
            return _photoAlbumContext.PhotoCollections.Include("Photos")
                                                      .Include("Place")
                                                      .SingleOrDefault(p => p.PhotoCollectionId == photoCollectionId);
        }

        public IEnumerable<PhotoCollection> GetPhotoCollections()
        {
            return _photoAlbumContext.PhotoCollections.Include("Photos")
                                                      .Include("Place");
        }

        public void Dispose()
        {
            _photoAlbumContext.Dispose();
        }
    }
}
