using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.DAL
{
    public interface IPhotoCollectionRepository
    {
        IEnumerable<PhotoCollection> GetPhotoCollections();
        PhotoCollection GetPhotoCollectionById(int photoCollectionId);
    }
}
