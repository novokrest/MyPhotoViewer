using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using System.Configuration;

namespace PhotoDiscoverService.Data
{
    internal class AlbumsLoader
    {
        private readonly IPhotosDbContext _context;
        private readonly string _rootDirectory;
        private readonly PlaceRegister _placeRegister;

        public static AlbumsLoader CreateFromConfiguration(IPhotosDbContext context)
        {
            string photosRootDirectoryPath = ConfigurationManager.AppSettings["PhotosRootDirectory"];
            return new AlbumsLoader(context, photosRootDirectoryPath);
        }

        public AlbumsLoader(IPhotosDbContext context, string rootDirectory)
        {
            _context = context;
            _rootDirectory = rootDirectory;
            _placeRegister = new PlaceRegister(context);
        }

        public void LoadPhotoAlbums()
        {
            foreach (string photoAblumDirectory in DirectoryInfoEx.GetChildDirectories(_rootDirectory))
            {
                var photoAlbumLoader = new AlbumLoader(_context, _placeRegister, photoAblumDirectory);
                photoAlbumLoader.Load();
            }
        }
    }
}
