using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using System.Collections.Generic;
using System.IO;

namespace PhotoDiscoverService.Data
{
    internal class PhotoImageSaver
    {
        private const int MaxCachedBytes = 200 * 1024 * 1024;
        private readonly List<PhotoEntity> _loadedPhotos = new List<PhotoEntity>();
        private int _loadedBytes = 0;

        private readonly IPhotosDbContext _context;
        private readonly PhotoAlbumEntity _album;
        private readonly IEnumerable<IPhotoImage> _photoImages;

        public PhotoImageSaver(IPhotosDbContext context, PhotoAlbumEntity album, IEnumerable<IPhotoImage> photoImages)
        {
            _context = context;
            _album = album;
            _photoImages = photoImages;
        }

        public void SavePhotos()
        {
            foreach(IPhotoImage photoImage in _photoImages)
            {
                LoadPhotoImage(photoImage);
                if (ShouldSaveLoadedPhotos())
                {
                    SaveLoadedPhotos();
                }
            }
            SaveLoadedPhotos();
        }

        private void LoadPhotoImage(IPhotoImage photoImage)
        {
            PhotoEntity photo = CreatePhotoFromPhotoImage(photoImage);
            _loadedBytes += photo.Image.Length;
            _loadedPhotos.Add(photo);
        }

        private bool ShouldSaveLoadedPhotos()
        {
            return _loadedBytes > MaxCachedBytes;
        }

        private void SaveLoadedPhotos()
        {
            _loadedPhotos.ForEach(photo => _context.Photos.Add(photo));
            _context.SaveChanges();
            _loadedPhotos.Clear();
        }

        private PhotoEntity CreatePhotoFromPhotoImage(IPhotoImage photoImage)
        {
            return new PhotoEntity
            {
                Title = new FileInfo(photoImage.Path).Name,
                CreationDate = photoImage.CreationDate,
                PhotoAlbumId = _album.Id,
                PlaceId = _album.Place.Id,
                Image = photoImage.Image,
                ImageType = ImageTypeRecognizer.Recognize(photoImage.Image)
            };
        }
    }
}
