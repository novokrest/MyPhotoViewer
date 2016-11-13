using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;
using System.Collections.Generic;
using System.Configuration;

namespace PhotoDiscoverService.Data
{
    class PhotoAlbumsLoader
    {
        public static IReadOnlyCollection<PhotoAlbumEntity> LoadPhotoAlbums()
        {
            string photosRootDirectoryPath = ConfigurationManager.AppSettings["PhotosRootDirectory"];
            var photosExplorer = new PhotosExplorer(photosRootDirectoryPath);

            return photosExplorer.GetPhotos();
        }
    }
}
