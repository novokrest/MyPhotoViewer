using MyPhotoViewer.DAL;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.ViewModels
{
    public class PhotoAlbumThumnailCreator
    {
        private readonly IEnumerable<PhotoAlbum> _photoAlbums;

        public PhotoAlbumThumnailCreator(IEnumerable<PhotoAlbum> photoAlbums)
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
                    PhotoAlbumId = photoAlbum.PhotoAlbumId,
                    Name = photoAlbum.Title,
                    Period = photoAlbum.Period,
                    Place = photoAlbum.Place,
                    Description = photoAlbum.Description,
                    Image = photoAlbum.Photos.First().PhotoId.ToString()
                };
            }
        }
    }
}