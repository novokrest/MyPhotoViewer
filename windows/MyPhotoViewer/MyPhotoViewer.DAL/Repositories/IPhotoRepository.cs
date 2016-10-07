using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.DAL
{
    public interface IPhotoRepository : IDisposable
    {
        IEnumerable<Photo> GetPhotos();
        Photo GetPhotoById(int photo);
        void InsertPhoto(Photo photo);
        void UpdatePhoto(Photo photo);
        void DeletePhoto(int photo);
        void Save();
    }
}
