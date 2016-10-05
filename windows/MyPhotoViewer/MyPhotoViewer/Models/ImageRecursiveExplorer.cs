using ExifLib;
using MyPhotoViewer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyPhotoViewer.Models
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
                CreationDate = createdDate
            };
        }

        private static DateTime ExtractCreatedDate(string imagePath)
        {
            //DateTime createdDate;
            //using (var exifReader = new ExifReader(imagePath))
            //{
            //    Verifiers.Verify(exifReader.GetTagValue(ExifTags.DateTime, out createdDate),
            //                     "Failed to extract information from image: {0}", imagePath);
            //}

            var imageFileInfo = new FileInfo(imagePath);

            return Comparator.Min(imageFileInfo.CreationTime, 
                                  imageFileInfo.LastAccessTime, 
                                  imageFileInfo.LastWriteTime);
        }

        private class PhotoImage : IPhotoImage
        {
            public string Path { get; set; }
            public DateTime CreationDate { get; set; }
        }
    }
}