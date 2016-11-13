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

        void AddPhoto(PhotoEntity photo);
        void UpdatePhoto(IPhoto photo);
        void DeletePhoto(int photo);
        void Save();
    }
}
