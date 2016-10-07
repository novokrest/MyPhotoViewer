using MyPhotoViewer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyPhotoViewer.DAL.Data
{
    internal class PhotoCollectionCreator
    {
        private readonly PhotoCollectionOverview _photosOverview;
        private readonly Place _place;

        public PhotoCollectionCreator(PhotoCollectionOverview photosOverview)
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

        public PhotoCollection CreatePhotoCollection(IReadOnlyCollection<IPhotoImage> photoImages)
        {
            var photos = photoImages.Select(CreatePhotoFromPhotoImage).ToList();

            return new PhotoCollection
            {
                Name = _photosOverview.Title,
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
                Path = photoImage.Path
            };
        }

        private static DateTime GetEarliestDate(IReadOnlyCollection<Photo> photos)
        {
            return photos.Select(photo => photo.CreationDate).Min();
        }

        private static DateTime GetLatestDate(IReadOnlyCollection<Photo> photos)
        {
            return photos.Select(photo => photo.CreationDate).Max();
        }
    }

}