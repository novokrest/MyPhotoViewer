using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoDiscoverService.Data
{
    internal class AlbumLoader
    {
        private readonly IPhotosDbContext _context;
        private readonly PlaceRegister _placeRegister;
        private readonly string _rootDirectory;

        public AlbumLoader(IPhotosDbContext context, PlaceRegister placeRegister, string rootDirectory)
        {
            Verifiers.ArgNullVerify(!rootDirectory.IsNullOrEmpty(), nameof(rootDirectory));
            Verifiers.Verify(Directory.Exists(rootDirectory), "Specified directory does not exist: {0}", rootDirectory);

            _context = context;
            _placeRegister = placeRegister;
            _rootDirectory = rootDirectory;
        }

        public void Load()
        {
            var albumOverviewReader = new AlbumOverviewReader(_rootDirectory);
            if (albumOverviewReader.ReadOverview())
            {
                CreatePhotoAlbum(albumOverviewReader.Overview);
            }
        }        

        private void CreatePhotoAlbum(AlbumOverview overview)
        {
            IEnumerable<IPhotoImage> photoImages = FindPhotoImages(_rootDirectory);
            if (photoImages.Any())
            {
                var photoAlbumCreator = new AlbumCreator(_context, overview, _placeRegister, photoImages);
                photoAlbumCreator.CreatePhotoAlbum();
            }
        }

        private IEnumerable<IPhotoImage> FindPhotoImages(string photosDirectoryPath)
        {
            var photoImageExplorer = new PhotoImageFinder(photosDirectoryPath);
            var photoImages = photoImageExplorer.FindPhotoImages();

            return photoImages;
        }
    }
}