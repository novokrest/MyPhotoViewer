using MyPhotoViewer.DAL.Entity;

namespace MyPhotoViewer.DAL
{
    public class RepositoryServiceLocator
    {
        public static IPhotoAlbumRepository GetPhotoAlbumRepository()
        {
            return new PhotoAlbumRepository(CreatePhotosContext());
        }

        public static IPhotoRepository GetPhotoRepository()
        {
            return new PhotoRepository(CreatePhotosContext());
        }

        private static PhotosDbContext CreatePhotosContext()
        {
            var photosContext = new PhotosDbContext();
            return photosContext;
        }
    }
}
