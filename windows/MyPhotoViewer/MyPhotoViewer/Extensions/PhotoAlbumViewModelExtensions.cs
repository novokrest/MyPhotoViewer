using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.ViewModels;
using System;
using System.Linq;

namespace MyPhotoViewer.Extensions
{
    public static class PhotoAlbumViewModelExtensions
    {
        public static PhotoAlbum ToPhotoAlbum(this NewPhotoAlbumViewModel photoAlbumViewModel)
        {
            var place = new Place
            {
                Name = photoAlbumViewModel.Place,
                City = photoAlbumViewModel.City,
                Country = photoAlbumViewModel.Country
            };

            var photos = photoAlbumViewModel.Photos.Select(httpFile => CreatePhoto(httpFile, place)).ToList();

            var photoAlbum = new PhotoAlbum
            {
                Title = photoAlbumViewModel.Title,
                Description = photoAlbumViewModel.Description,
                Period = new DateTimePeriod
                {
                    From = photoAlbumViewModel.From ?? DateTime.MinValue,
                    To = photoAlbumViewModel.To ?? DateTime.MaxValue
                },
                Place = place,
                Photos = photos
            };

            return photoAlbum;
        }

        private static Photo CreatePhoto(IHttpFile httpFile, Place place)
        {
            return new Photo
            {
                Title = httpFile.FileName,
                Image = httpFile.Data,
                Place = place
            };
        }
    }
}
