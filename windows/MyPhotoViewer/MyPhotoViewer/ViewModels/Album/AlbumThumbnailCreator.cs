using MyPhotoViewer.Core;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.ViewModels.Album
{
    public class AlbumThumbnailCreator
    {
        private readonly IEnumerable<IAlbum> _albums;

        public AlbumThumbnailCreator(IEnumerable<IAlbum> photoAlbums)
        {
            _albums = photoAlbums;
        }
            
        public IReadOnlyList<IAlbumThumbnail> CreateThumbnails()
        {
            return CreateThumbnailsLazy().ToList();
        }

        private IEnumerable<IAlbumThumbnail> CreateThumbnailsLazy()
        {
            foreach(var album in _albums)
            {
                IReadOnlyCollection<int> photoIds = album.GetPhotoIds();

                yield return new AlbumThumbnail()
                {
                    AlbumId = album.Id,
                    Name = album.Title,
                    Period = album.Period,
                    Place = album.Place,
                    Description = album.Description,
                    CoverPhotoId = photoIds.GetRandom(),
                    PhotosCount = photoIds.Count
                };
            }
        }
    }
}