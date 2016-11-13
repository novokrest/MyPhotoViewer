﻿using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPhotoViewer.Converters
{
    public class PhotoAlbumCreator
    {
        private readonly NewPhotoAlbumViewModel _photoAlbumViewModel;

        public static PhotoAlbumEntity ConvertToPhotoAlbum(NewPhotoAlbumViewModel photoAlbumViewModel)
        {
            return new PhotoAlbumCreator(photoAlbumViewModel).CreatePhotoAlbum();
        }

        public PhotoAlbumCreator(NewPhotoAlbumViewModel photoAlbumViewModel)
        {
            _photoAlbumViewModel = photoAlbumViewModel;
        }

        public PhotoAlbumEntity CreatePhotoAlbum()
        {
            string title = ExtractTitle();
            string description = ExtractDescription();
            PlaceEntity place = ExtractPlace();
            DateTimePeriod period = ExtractPeriod();
            ICollection<PhotoEntity> photos = ExtractPhotos(place);

            return new PhotoAlbumEntity
            {
                Title = title,
                Description = description,
                Place = place,
                Period = period,
                Photos = photos
            };
        }

        private string ExtractTitle()
        {
            return _photoAlbumViewModel.Title;
        }

        private string ExtractDescription()
        {
            return _photoAlbumViewModel.Description;
        }

        private PlaceEntity ExtractPlace()
        {
            return new PlaceEntity
            {
                Name = _photoAlbumViewModel.Place,
                City = _photoAlbumViewModel.City,
                Country = _photoAlbumViewModel.Country
            };
        }

        private DateTimePeriod ExtractPeriod()
        {
            var period = new DateTimePeriod();

            if (_photoAlbumViewModel.From.HasValue)
            {
                period.From = _photoAlbumViewModel.From.Value;
            }
            if (_photoAlbumViewModel.To.HasValue)
            {
                period.To = _photoAlbumViewModel.To.Value;
            }

            return period;
        }

        private ICollection<PhotoEntity> ExtractPhotos(PlaceEntity place)
        {
            return _photoAlbumViewModel.Photos.Select(httpFile => CreatePhoto(httpFile, place)).ToList();
        }

        private static PhotoEntity CreatePhoto(IHttpFile httpFile, PlaceEntity place)
        {
            return new PhotoEntity
            {
                Title = httpFile.FileName,
                Image = httpFile.Data,
                ImageType = ImageMimeTypeConverter.ToImageType(httpFile.ContentType),
                Place = place
            };
        }
    }
}