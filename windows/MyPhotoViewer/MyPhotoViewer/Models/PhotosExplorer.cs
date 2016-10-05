using MyPhotoViewer.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyPhotoViewer.Models
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

        public IReadOnlyCollection<PhotoCollection> GetPhotos()
        {
            return GetPhotosLazy().ToList();
        }

        private IEnumerable<PhotoCollection> GetPhotosLazy()
        {
            foreach (DirectoryInfo photoDirectory in GetChildPhotoDirectories())
            {
                PhotoCollectionOverview photosOverview;
                if (PhotoCollectionOverviewReader.TryReadOverview(photoDirectory.FullName, out photosOverview))
                {
                    var photoCollectionCreator = new PhotoCollectionCreator(photosOverview);
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