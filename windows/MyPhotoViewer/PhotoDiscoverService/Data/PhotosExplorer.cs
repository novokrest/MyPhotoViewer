using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoDiscoverService.Data
{
    internal class PhotosExplorer
    {
        private readonly string _rootDirectory;

        public PhotosExplorer(string directoryPath)
        {
            Verifiers.ArgNullVerify(!directoryPath.IsNullOrEmpty(), nameof(directoryPath));
            Verifiers.Verify(Directory.Exists(directoryPath), "Specified directory does not exist: {0}", directoryPath);

            _rootDirectory = directoryPath;
        }

        public IReadOnlyCollection<PhotoAlbum> GetPhotos()
        {
            return GetPhotosLazy().ToList();
        }

        private IEnumerable<PhotoAlbum> GetPhotosLazy()
        {
            foreach (DirectoryInfo photoDirectory in GetChildPhotoDirectories())
            {
                PhotoAlbumOverview photosOverview;
                if (PhotoCollectionOverviewReader.TryReadOverview(photoDirectory.FullName, out photosOverview))
                {
                    var photoCollectionCreator = new PhotoAlbumCreator(photosOverview);
                    var photoImages = FindPhotoImages(photoDirectory.FullName);

                    yield return photoCollectionCreator.CreatePhotoCollection(photoImages);
                }
            }
        }

        private IEnumerable<DirectoryInfo> GetChildPhotoDirectories()
        {
            return new DirectoryInfo(_rootDirectory).GetDirectories();
        }

        private IReadOnlyCollection<IPhotoImage> FindPhotoImages(string photosDirectoryPath)
        {
            var photoImageExplorer = new PhotoImageRecursiveExplorer(photosDirectoryPath);
            var photoImages = photoImageExplorer.ExplorePhotoImages();

            return photoImages;
        }
    }
}