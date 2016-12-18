using MyPhotoViewer.Core;
using System.Collections.Generic;
using System.Linq;


namespace MyPhotoViewer.DAL.Extensions
{
    public static class PhotoAlbumRepositoryExtension
    {
        public static IReadOnlyList<IAlbum> GetRandomAlbums(this IAlbumRepository albumRepository, int count)
        {
            IReadOnlyList<IAlbum> albums = albumRepository.LoadAlbums().ToList();
            return GetRandomAlbums(albums, count).ToList();
        }

        private static IEnumerable<IAlbum> GetRandomAlbums(IReadOnlyList<IAlbum> albums, int count)
        {
            foreach (int index in RandomEx.NonRepeatableRandoms(0, albums.Count, count))
            {
                yield return albums[index];
            }
        }
    }
}
