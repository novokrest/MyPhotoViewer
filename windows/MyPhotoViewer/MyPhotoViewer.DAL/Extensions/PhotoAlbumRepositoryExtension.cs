using MyPhotoViewer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MyPhotoViewer.DAL.Extensions
{
    public static class PhotoAlbumRepositoryExtension
    {
        public static IReadOnlyList<PhotoAlbum> GetRandomPhotoAlbums(this IPhotoAlbumRepository photoAlbumRepository, int count)
        {
            IReadOnlyList<PhotoAlbum> photoAlbums = photoAlbumRepository.GetPhotoAlbums().ToList();
            return GetRandomPhotoAlbums(photoAlbums, count).ToList();
        }

        private static IEnumerable<PhotoAlbum> GetRandomPhotoAlbums(IReadOnlyList<PhotoAlbum> photoAlbums, int count)
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
