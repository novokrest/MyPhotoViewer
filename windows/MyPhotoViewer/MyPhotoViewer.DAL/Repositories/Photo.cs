using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using System;
using System.Linq;


namespace MyPhotoViewer.DAL.Repositories
{
    class Photo : IPhoto
    {
        private readonly IPhotosContext _photosContext;
        private readonly int _photoId;
        private readonly PhotoEntity _photoEntity;

        public Photo(IPhotosContext photosContext, int photoId)
        {
            _photosContext = photosContext;
            _photoId = photoId;
            _photoEntity = _photosContext.Photos.First(photo => photo.Id == photoId);
        }

        public int Id => _photoEntity.Id;
        public int PhotoAlbumId => _photoEntity.PhotoAlbumId; 
        public string Title => _photoEntity.Title;
        public DateTime? CreationDate => _photoEntity.CreationDate;

        public Image GetImage()
        {
            return new Image(_photoEntity.Image, _photoEntity.ImageType.Value);
        }
    }
}
