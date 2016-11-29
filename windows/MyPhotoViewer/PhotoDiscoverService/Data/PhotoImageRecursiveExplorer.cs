using MyPhotoViewer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoDiscoverService.Data
{
    internal class PhotoImageFinder
    {
        private static readonly string[] ImagesSearchPatterns = { "*.jpg" , "*.png" };

        private readonly string _rootDirectory;

        public PhotoImageFinder(string rootDirectory)
        {
            Verifiers.ArgNullVerify(rootDirectory, nameof(rootDirectory));

            _rootDirectory = rootDirectory;
        }

        public IEnumerable<IPhotoImage> FindPhotoImages()
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
            return new PhotoImage(imagePath, ExtractCreatedDate(imagePath));
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
            public PhotoImage(string path, DateTime creationDate)
            {
                Path = path;
                CreationDate = creationDate;
            }

            public string Path { get; }
            public DateTime CreationDate { get; }

            public byte[] Image => File.ReadAllBytes(Path);
        }
    }
}