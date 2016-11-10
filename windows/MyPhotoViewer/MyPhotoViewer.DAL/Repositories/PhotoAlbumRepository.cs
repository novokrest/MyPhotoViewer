using MyPhotoViewer.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyPhotoViewer.DAL
{
    internal class PhotoAlbumRepository : IPhotoAlbumRepository, IDisposable
    {
        private readonly PhotosContext _photoAlbumContext;

        public PhotoAlbumRepository(PhotosContext photoAlbumContext)
        {
            Verifiers.ArgNullVerify(photoAlbumContext, nameof(photoAlbumContext));
            _photoAlbumContext = photoAlbumContext;
        }

        public PhotoAlbum GetPhotoAlbumById(int photoCollectionId)
        {
            return _photoAlbumContext.PhotoAlbums.Include("Photos")
                                                 .Include("Place")
                                                 .SingleOrDefault(p => p.PhotoAlbumId == photoCollectionId);
        }

        public IEnumerable<PhotoAlbum> GetPhotoAlbums()
        {
            return _photoAlbumContext.PhotoAlbums.Include("Photos")
                                                 .Include("Place");
        }

        public void AddPhotoAlbum(PhotoAlbum photoAlbum)
        {
            _photoAlbumContext.PhotoAlbums.Add(photoAlbum);
            _photoAlbumContext.SaveChanges();
        }

        public void SavePhotoAlbum(PhotoAlbum photoAlbum)
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
            PhotoAlbum entry = _photoAlbumContext.PhotoAlbums.Find(photoAlbumId);
            if (entry != null)
            {
                _photoAlbumContext.PhotoAlbums.Remove(entry);
                _photoAlbumContext.SaveChanges();
            }
        }

        public void Save()
        {
            _photoAlbumContext.SaveChanges();
        }

        public void Dispose()
        {
            _photoAlbumContext.Dispose();
        }
    }
}
