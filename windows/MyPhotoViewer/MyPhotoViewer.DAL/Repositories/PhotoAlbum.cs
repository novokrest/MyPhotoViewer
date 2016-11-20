using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.DAL.Repositories
{
    class PhotoAlbum : IPhotoAlbum
    {
        private readonly IPhotosDbContext _photosContext;
        private readonly int _photoAlbumId;
        private readonly PhotoAlbumEntity _photoAlbum;

        public PhotoAlbum(IPhotosDbContext photosContext, int photoAlbumId)
        {
            _photosContext = photosContext;
            _photoAlbumId = photoAlbumId;
            _photoAlbum = photosContext.PhotoAlbums.First(photoAlbum => photoAlbum.Id == photoAlbumId);
        }

        public int Id => _photoAlbum.Id;
        public string Title => _photoAlbum.Title;
        public string Description => _photoAlbum.Description;
        public IDateTimePeriod Period => _photoAlbum.Period;
        public IPlace Place => _photoAlbum.Place;

        public IReadOnlyCollection<int> GetPhotoIds()
        {
            return _photosContext.Photos.Where(photo => photo.PhotoAlbumId == _photoAlbumId)
                                        .Select(photo => photo.Id)
                                        .ToList();
        }

        public Image GetPhotoImage(int photoId)
        {
            return new Photo(_photosContext, photoId).GetImage();
        }
    }
}
