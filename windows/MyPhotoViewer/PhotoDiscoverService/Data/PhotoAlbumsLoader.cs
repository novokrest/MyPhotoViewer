using MyPhotoViewer.DAL;
using System.Collections.Generic;
using System.Configuration;

namespace PhotoDiscoverService.Data
{
    class PhotoAlbumsLoader
    {
        public static IReadOnlyCollection<PhotoAlbum> LoadPhotoAlbums()
        {
            string photosRootDirectoryPath = ConfigurationManager.AppSettings["PhotosRootDirectory"];
            var photosExplorer = new PhotosExplorer(photosRootDirectoryPath);

            return photosExplorer.GetPhotos();
        }
    }
}
