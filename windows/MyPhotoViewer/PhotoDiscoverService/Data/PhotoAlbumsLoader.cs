using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using System.Configuration;

namespace PhotoDiscoverService.Data
{
    internal class PhotoAlbumsLoader
    {
        private readonly IPhotosDbContext _context;
        private readonly string _rootDirectory;
        private readonly PlaceRegister _placeRegister;

        public static PhotoAlbumsLoader CreateFromConfiguration(IPhotosDbContext context)
        {
            string photosRootDirectoryPath = ConfigurationManager.AppSettings["PhotosRootDirectory"];
            return new PhotoAlbumsLoader(context, photosRootDirectoryPath);
        }

        public PhotoAlbumsLoader(IPhotosDbContext context, string rootDirectory)
        {
            _context = context;
            _rootDirectory = rootDirectory;
            _placeRegister = new PlaceRegister(context);
        }

        public void LoadPhotoAlbums()
        {
            foreach (string photoAblumDirectory in DirectoryInfoEx.GetChildDirectories(_rootDirectory))
            {
                var photoAlbumLoader = new PhotoAlbumLoader(_context, _placeRegister, photoAblumDirectory);
                photoAlbumLoader.Load();
            }
        }
    }
}
