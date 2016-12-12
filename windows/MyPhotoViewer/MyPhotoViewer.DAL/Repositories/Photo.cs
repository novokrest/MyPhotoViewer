using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using System;
using System.Linq;


namespace MyPhotoViewer.DAL.Repositories
{
    class Photo : IPhoto, IUpdatablePhoto
    {
        private readonly IPhotosDbContext _photosContext;
        private readonly int _photoId;
        private readonly PhotoEntity _photoEntity;

        public Photo(IPhotosDbContext photosContext, int photoId)
        {
            _photosContext = photosContext;
            _photoId = photoId;
            _photoEntity = _photosContext.Photos.First(photo => photo.Id == photoId);
        }

        public int Id => _photoEntity.Id;

        public int PhotoAlbumId
        {
            get { return _photoEntity.PhotoAlbumId; }
            set { _photoEntity.PhotoAlbumId = value; }
        }

        public IAlbum PhotoAlbum => new Album(_photosContext, _photoEntity.PhotoAlbumId);
            
        public string Title
        {
            get { return _photoEntity.Title; }
            set { _photoEntity.Title = value; }
        } 

        public DateTime? CreationDate
        {
            get { return _photoEntity.CreationDate; }
            set { _photoEntity.CreationDate = value; }
        }

        public void Update()
        {
            _photosContext.Entry(_photoEntity).State = System.Data.Entity.EntityState.Modified;
            _photosContext.SaveChanges();
        }

        public Image GetImage()
        {
            return new Image(_photoEntity.Image, _photoEntity.ImageType.Value);
        }
    }
}
