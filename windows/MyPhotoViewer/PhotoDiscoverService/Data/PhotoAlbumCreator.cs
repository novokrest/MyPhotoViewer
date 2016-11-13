using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoDiscoverService.Data
{
    internal class PhotoAlbumCreator
    {
        private readonly PhotoAlbumOverview _photosOverview;
        private readonly PlaceEntity _place;

        public PhotoAlbumCreator(PhotoAlbumOverview photosOverview)
        {
            Verifiers.ArgNullVerify(photosOverview, nameof(photosOverview));

            _photosOverview = photosOverview;
            _place = new PlaceEntity
            {
                Name = _photosOverview.Place,
                City = _photosOverview.City,
                Country = _photosOverview.Country
            };
        }

        public PhotoAlbumEntity CreatePhotoCollection(IReadOnlyCollection<IPhotoImage> photoImages)
        {
            var photos = photoImages.Select(CreatePhotoFromPhotoImage).ToList();

            return new PhotoAlbumEntity
            {
                Title = _photosOverview.Title,
                Description = _photosOverview.Description,
                Place = _place,
                Period = new DateTimePeriod
                {
                    From = _photosOverview.From ?? GetEarliestDate(photos),
                    To = _photosOverview.From ?? GetLatestDate(photos)
                },
                Photos = photos
            };
        }

        private PhotoEntity CreatePhotoFromPhotoImage(IPhotoImage photoImage)
        {
            return new PhotoEntity
            {
                Title = new FileInfo(photoImage.Path).Name,
                CreationDate = photoImage.CreationDate,
                Place = _place,
                Image = photoImage.Image,
                ImageType = ImageTypeRecognizer.Recognize(photoImage.Image)
            };
        }

        private static DateTime GetEarliestDate(IReadOnlyCollection<PhotoEntity> photos)
        {
            return GetPhotoCreationDates(photos).Min();
        }

        private static DateTime GetLatestDate(IReadOnlyCollection<PhotoEntity> photos)
        {
            return GetPhotoCreationDates(photos).Max();
        }

        private static IEnumerable<DateTime> GetPhotoCreationDates(IReadOnlyCollection<PhotoEntity> photos)
        {
            return photos.Where(photo => photo.CreationDate.HasValue).Select(photo => photo.CreationDate.Value);
        }
    }

}