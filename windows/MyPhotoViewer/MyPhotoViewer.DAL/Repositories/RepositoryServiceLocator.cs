using MyPhotoViewer.DAL.Entity;

namespace MyPhotoViewer.DAL
{
    public class RepositoryServiceLocator
    {
        public static IAlbumRepository GetPhotoAlbumRepository()
        {
            return new AlbumRepository(CreatePhotosContext());
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
