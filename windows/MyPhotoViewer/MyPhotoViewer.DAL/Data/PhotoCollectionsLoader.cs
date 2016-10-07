using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.DAL.Data
{
    class PhotoCollectionsLoader
    {
        public static IReadOnlyCollection<PhotoCollection> LoadPhotoCollections()
        {
            string photosRootDirectoryPath = @"E:\Github\MyPhotoViewer\photos-test"; //System.Configuration.ConfigurationManager.AppSettings["PhotosRootDirectory"];
            var photosExplorer = new PhotosExplorer(photosRootDirectoryPath);

            return photosExplorer.GetPhotos();
        }
    }
}
