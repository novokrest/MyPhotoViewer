using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.DAL.Repositories;
using System;
using System.Collections.Generic;

namespace MyPhotoViewer.DAL
{
    public interface IPhotoRepository : IDisposable
    {
        IEnumerable<IPhoto> GetPhotos();

        IPhoto GetPhotoById(int photo);
        IUpdatablePhoto GetUpdatablePhotoById(int photo);
        Image GetPhotoImage(int photoId);

        void AddPhoto(PhotoEntity photo);
        void UpdatePhoto(IUpdatablePhoto photo);



        void DeletePhoto(int photo);
        void Save();
    }
}
