namespace MyPhotoViewer.DAL
{
    public class RepositoryServiceLocator
    {
        public static IPhotoCollectionRepository GetPhotoCollectionRepository()
        {
            return new PhotoCollectionRepository(new PhotoAlbumContext());
        }

        public static IPhotoRepository GetPhotoRepository()
        {
            return new PhotoRepository(new PhotoAlbumContext());
        }
    }
}
