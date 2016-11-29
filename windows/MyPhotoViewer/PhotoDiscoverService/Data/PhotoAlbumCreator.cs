using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoDiscoverService.Data
{
    internal class PhotoAlbumCreator
    {
        private readonly IPhotosDbContext _context;
        private readonly PhotoAlbumOverview _photosOverview;
        private readonly PlaceEntity _place;
        private readonly IEnumerable<IPhotoImage> _photoImages;

        public PhotoAlbumCreator(IPhotosDbContext context, PhotoAlbumOverview photosOverview, PlaceRegister placeRegister, IEnumerable<IPhotoImage> photoImages)
        {
            Verifiers.ArgNullVerify(photosOverview, nameof(photosOverview));

            _context = context;
            _photosOverview = photosOverview;
            _place = placeRegister.Register(_photosOverview.Place, _photosOverview.City, _photosOverview.Country);
            _photoImages = photoImages;
        }

        public void CreatePhotoAlbum()
        {
            PhotoAlbumEntity album = CreateEmptyPhotoAlbum();
            SavePhotos(album);
        }

        private PhotoAlbumEntity CreateEmptyPhotoAlbum()
        {
            var album = new PhotoAlbumEntity
            {
                Title = _photosOverview.Title,
                Description = _photosOverview.Description,
                PlaceId = _place.Id,
                Period = new DateTimePeriod
                {
                    From = _photosOverview.From ?? GetPhotosEarliestDate(),
                    To = _photosOverview.From ?? GetPhotosLatestDate()
                }
            };

            album = _context.PhotoAlbums.Add(album);
            _context.SaveChanges();

            return album;
        }

        private void SavePhotos(PhotoAlbumEntity album)
        {
            var photoImageSaver = new PhotoImageSaver(_context, album, _photoImages);
            photoImageSaver.SavePhotos();
        }

        private DateTime GetPhotosEarliestDate()
        {
            return GetPhotoCreationDates().Min();
        }

        private DateTime GetPhotosLatestDate()
        {
            return GetPhotoCreationDates().Max();
        }

        private IEnumerable<DateTime> GetPhotoCreationDates()
        {
            return _photoImages.Select(photo => photo.CreationDate);
        }
    }

}