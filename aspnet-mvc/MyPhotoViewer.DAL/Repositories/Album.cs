using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.DAL.Repositories
{
    class Album : IAlbum
    {
        private readonly IPhotosDbContext _photosContext;
        private readonly int _photoAlbumId;
        private readonly AlbumEntity _photoAlbum;

        public Album(IPhotosDbContext photosContext, int photoAlbumId)
        {
            _photosContext = photosContext;
            _photoAlbumId = photoAlbumId;
            _photoAlbum = photosContext.Albums.First(photoAlbum => photoAlbum.Id == photoAlbumId);
        }

        public int Id => _photoAlbum.Id;
        public string Title => _photoAlbum.Title;
        public string Description => _photoAlbum.Description;
        public IDateTimePeriod Period => _photoAlbum.Period;
        public IPlace Place => _photoAlbum.Place;

        public IReadOnlyCollection<int> GetPhotoIds()
        {
            return _photosContext.Photos.Where(photo => photo.AlbumId == _photoAlbumId)
                                        .Select(photo => photo.Id)
                                        .ToList();
        }

        public Image GetPhotoImage(int photoId)
        {
            return new Photo(_photosContext, photoId).GetImage();
        }
    }
}
