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

        private static PhotosContext CreatePhotosContext()
        {
            var photosContext = new PhotosContext();
            return photosContext;
        }
    }
}
