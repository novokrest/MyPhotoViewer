using MyPhotoViewer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoDiscoverService.Data
{
    internal class PhotoImageRecursiveExplorer
    {
        private static readonly string[] ImagesSearchPatterns = { "*.jpg" , "*.png" };

        private readonly string _rootDirectory;

        public PhotoImageRecursiveExplorer(string rootDirectory)
        {
            Verifiers.ArgNullVerify(rootDirectory, nameof(rootDirectory));

            _rootDirectory = rootDirectory;
        }

        public IReadOnlyCollection<IPhotoImage> ExplorePhotoImages()
        {
            return ExplorePhotoImagesLazy().ToList();
        }

        private IEnumerable<IPhotoImage> ExplorePhotoImagesLazy()
        {
            foreach (FileInfo imageInfo in FindImages())
            {
                yield return ExtractPhotoImage(imageInfo.FullName);
            }
        }

        private IEnumerable<FileInfo> FindImages()
        {
            var rootDirectoryInfo = new DirectoryInfo(_rootDirectory);

            return ImagesSearchPatterns.SelectMany(imageSearchPattern => 
                                                   rootDirectoryInfo.EnumerateFiles(imageSearchPattern, SearchOption.AllDirectories));
        }

        private IPhotoImage ExtractPhotoImage(string imagePath)
        {
            DateTime createdDate = ExtractCreatedDate(imagePath);

            return new PhotoImage
            {
                Path = imagePath,
                CreationDate = createdDate,
                Image = File.ReadAllBytes(imagePath)
            };
        }

        private static DateTime ExtractCreatedDate(string imagePath)
        {
            var imageFileInfo = new FileInfo(imagePath);

            return Comparator.Min(imageFileInfo.CreationTime, 
                                  imageFileInfo.LastAccessTime, 
                                  imageFileInfo.LastWriteTime);
        }

        private class PhotoImage : IPhotoImage
        {
            public string Path { get; set; }
            public DateTime CreationDate { get; set; }
            public byte[] Image { get; set; }
        }
    }
}