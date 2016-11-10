using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoDiscoverService.Data
{
    internal class PhotoAlbumCreator
    {
        private readonly PhotoAlbumOverview _photosOverview;
        private readonly Place _place;

        public PhotoAlbumCreator(PhotoAlbumOverview photosOverview)
        {
            Verifiers.ArgNullVerify(photosOverview, nameof(photosOverview));

            _photosOverview = photosOverview;
            _place = new Place
            {
                Name = _photosOverview.Place,
                City = _photosOverview.City,
                Country = _photosOverview.Country
            };
        }

        public PhotoAlbum CreatePhotoCollection(IReadOnlyCollection<IPhotoImage> photoImages)
        {
            var photos = photoImages.Select(CreatePhotoFromPhotoImage).ToList();

            return new PhotoAlbum
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

        private Photo CreatePhotoFromPhotoImage(IPhotoImage photoImage)
        {
            return new Photo
            {
                Title = new FileInfo(photoImage.Path).Name,
                CreationDate = photoImage.CreationDate,
                Place = _place,
                Path = photoImage.Path,
                Image = photoImage.Image
            };
        }

        private static DateTime GetEarliestDate(IReadOnlyCollection<Photo> photos)
        {
            return GetPhotoCreationDates(photos).Min();
        }

        private static DateTime GetLatestDate(IReadOnlyCollection<Photo> photos)
        {
            return GetPhotoCreationDates(photos).Max();
        }

        private static IEnumerable<DateTime> GetPhotoCreationDates(IReadOnlyCollection<Photo> photos)
        {
            return photos.Where(photo => photo.CreationDate.HasValue).Select(photo => photo.CreationDate.Value);
        }
    }

}