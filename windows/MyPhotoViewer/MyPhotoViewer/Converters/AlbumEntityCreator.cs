using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.ViewModels.Album;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.Converters
{
    public class AlbumEntityCreator
    {
        private readonly BaseAlbumViewModel _abumViewModel;

        public static AlbumEntity FromNewAlbumViewModel(NewAlbumViewModel albumViewModel)
        {
            var albumEntity = new AlbumEntityCreator(albumViewModel).CreateAlbumEntity();
            albumEntity.Photos = ExtractPhotos(albumViewModel, albumEntity.Place);

            return albumEntity;
        }

        public static AlbumEntity FromEditAlbumViewModel(EditAlbumViewModel albumViewModel)
        {
            return new AlbumEntityCreator(albumViewModel).CreateAlbumEntity();
        }

        public AlbumEntityCreator(BaseAlbumViewModel albumViewModel)
        {
            _abumViewModel = albumViewModel;
        }

        public AlbumEntity CreateAlbumEntity()
        {
            int id = _abumViewModel.Id;
            string title = ExtractTitle();
            string description = ExtractDescription();
            PlaceEntity place = ExtractPlace();
            DateTimePeriod period = ExtractPeriod();

            return new AlbumEntity
            {
                Id = id,
                Title = title,
                Description = description,
                Place = place,
                Period = period
            };
        }

        private string ExtractTitle()
        {
            return _abumViewModel.Title;
        }

        private string ExtractDescription()
        {
            return _abumViewModel.Description;
        }

        private PlaceEntity ExtractPlace()
        {
            return new PlaceEntity
            {
                Name = _abumViewModel.Place,
                City = _abumViewModel.City,
                Country = _abumViewModel.Country
            };
        }

        private DateTimePeriod ExtractPeriod()
        {
            var period = new DateTimePeriod();

            if (_abumViewModel.From.HasValue)
            {
                period.From = _abumViewModel.From.Value;
            }
            if (_abumViewModel.To.HasValue)
            {
                period.To = _abumViewModel.To.Value;
            }

            return period;
        }

        private static ICollection<PhotoEntity> ExtractPhotos(NewAlbumViewModel albumViewModel, PlaceEntity placeEntity)
        {
            return albumViewModel.Photos.Select(httpFile => CreatePhoto(httpFile, placeEntity)).ToList();
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