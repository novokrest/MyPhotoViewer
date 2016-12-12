using MyPhotoViewer.Core;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MyPhotoViewer.DAL.Extensions
{
    public static class PhotoAlbumRepositoryExtension
    {
        public static IReadOnlyList<IAlbum> GetRandomPhotoAlbums(this IAlbumRepository photoAlbumRepository, int count)
        {
            IReadOnlyList<IAlbum> photoAlbums = photoAlbumRepository.LoadAlbums().ToList();
            return GetRandomPhotoAlbums(photoAlbums, count).ToList();
        }

        private static IEnumerable<IAlbum> GetRandomPhotoAlbums(IReadOnlyList<IAlbum> photoAlbums, int count)
        {
            var random = new Random();
            for (int i = 0; i < count; ++i)
            {
                int randomIndex = random.Next(0, photoAlbums.Count);
                yield return photoAlbums[randomIndex];
            }
        }
    }
}
