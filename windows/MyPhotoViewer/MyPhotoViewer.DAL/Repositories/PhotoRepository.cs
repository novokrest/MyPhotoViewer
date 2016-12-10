using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.DAL
{
    public sealed class PhotoRepository : IPhotoRepository
    {
        private readonly PhotosDbContext _photosContext;

        public PhotoRepository(PhotosDbContext photosContext)
        {
            _photosContext = photosContext;
        }

        public IPhoto GetPhotoById(int photoId)
        {
            return CreatePhoto(photoId);
        }

        public IUpdatablePhoto GetUpdatablePhotoById(int photoId)
        {
            return CreatePhoto(photoId);
        }

        private Photo CreatePhoto(int photoId)
        {
            return new Photo(_photosContext, photoId);
        }

        public IEnumerable<IPhoto> GetPhotos()
        {
            return _photosContext.Photos.AsEnumerable().Select(entity => new Photo(_photosContext, entity.Id));
        }

        public void AddPhoto(PhotoEntity photo)
        {
            _photosContext.Photos.Add(photo);
            _photosContext.SaveChanges();
        }

        public void UpdatePhoto(IUpdatablePhoto photo)
        {
            photo.Update();
        }

        public Image GetPhotoImage(int photoId)
        {
            return CreatePhoto(photoId).GetImage();
        }

        public void DeletePhoto(int photoId)
        {
            PhotoEntity photoEntity = _photosContext.Photos.Where(photo => photo.Id == photoId).Single();
            _photosContext.Entry(photoEntity).State = System.Data.Entity.EntityState.Deleted;
            _photosContext.SaveChanges();

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
