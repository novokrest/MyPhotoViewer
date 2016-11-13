using MyPhotoViewer.Core;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.ViewModels
{
    public class PhotoAlbumThumbnailCreator
    {
        private readonly IEnumerable<IPhotoAlbum> _photoAlbums;

        public PhotoAlbumThumbnailCreator(IEnumerable<IPhotoAlbum> photoAlbums)
        {
            _photoAlbums = photoAlbums;
        }
            
        public IReadOnlyList<IPhotoAlbumThumbnail> CreateThumbnails()
        {
            return CreateThumbnailsLazy().ToList();
        }

        private IEnumerable<IPhotoAlbumThumbnail> CreateThumbnailsLazy()
        {
            foreach(var photoAlbum in _photoAlbums)
            {
                yield return new PhotoAlbumThumbnail()
                {
                    PhotoAlbumId = photoAlbum.Id,
                    Name = photoAlbum.Title,
                    Period = photoAlbum.Period,
                    Place = photoAlbum.Place,
                    Description = photoAlbum.Description,
                    CoverPhotoId = photoAlbum.GetPhotoIds().GetRandom()
                };
            }
        }
    }
}