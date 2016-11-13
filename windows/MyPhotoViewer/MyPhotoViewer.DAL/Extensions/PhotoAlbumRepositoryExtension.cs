using MyPhotoViewer.Core;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MyPhotoViewer.DAL.Extensions
{
    public static class PhotoAlbumRepositoryExtension
    {
        public static IReadOnlyList<IPhotoAlbum> GetRandomPhotoAlbums(this IPhotoAlbumRepository photoAlbumRepository, int count)
        {
            IReadOnlyList<IPhotoAlbum> photoAlbums = photoAlbumRepository.GetPhotoAlbums().ToList();
            return GetRandomPhotoAlbums(photoAlbums, count).ToList();
        }

        private static IEnumerable<IPhotoAlbum> GetRandomPhotoAlbums(IReadOnlyList<IPhotoAlbum> photoAlbums, int count)
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
