using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.DAL
{
    internal class PhotoAlbumRepository : IPhotoAlbumRepository, IDisposable
    {
        private readonly PhotosContext _photosContext;

        public PhotoAlbumRepository(PhotosContext photosContext)
        {
            Verifiers.ArgNullVerify(photosContext, nameof(photosContext));
            _photosContext = photosContext;
        }

        public IPhotoAlbum GetPhotoAlbumById(int photoAlbumId)
        {
            return new PhotoAlbum(_photosContext, photoAlbumId);
        }

        public IEnumerable<IPhotoAlbum> GetPhotoAlbums()
        {
            IEnumerable<int> photoAlbumIds = _photosContext.PhotoAlbums.Select(photoAlbum => photoAlbum.Id);
            return photoAlbumIds.Select(photoAlbumId => new PhotoAlbum(_photosContext, photoAlbumId));
        }

        public void AddPhotoAlbum(PhotoAlbumEntity photoAlbum)
        {
            _photosContext.PhotoAlbums.Add(photoAlbum);
            _photosContext.SaveChanges();
        }

        public void SavePhotoAlbum(PhotoAlbumEntity photoAlbum)
        {
            //if (photoAlbum.PhotoAlbumId == 0)
            //{
            //    _photoAlbumContext.PhotoAlbums.Add(photoAlbum);
            //}
            //else
            //{
            //    _photoAlbumContext.Entry(photoAlbum).State = EntityState.Modified;
            //}

            //_photoAlbumContext.SaveChanges();
        }

        public void DeletePhotoAlbum(int photoAlbumId)
        {
            throw new NotImplementedException();
            //IPhotoAlbum entry = _photosContext.PhotoAlbums.Find(photoAlbumId);
            //if (entry != null)
            //{
            //    _photosContext.PhotoAlbums.Remove(entry);
            //    _photosContext.SaveChanges();
            //}
        }

        public void Save()
        {
            _photosContext.SaveChanges();
        }

        public void Dispose()
        {
            _photosContext.Dispose();
        }
    }
}
