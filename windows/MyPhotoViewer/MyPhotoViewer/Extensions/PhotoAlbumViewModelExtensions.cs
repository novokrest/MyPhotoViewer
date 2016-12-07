using MyPhotoViewer.Converters;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.ViewModels;

namespace MyPhotoViewer.Extensions
{
    public static class PhotoAlbumViewModelExtensions
    {
        public static PhotoAlbumEntity ToPhotoAlbum(this NewPhotoAlbumViewModel photoAlbumViewModel)
        {
            return PhotoAlbumConverter.ConvertToPhotoAlbum(photoAlbumViewModel);
        }

        public static PhotoAlbumEntity ToPhotoAlbum(this PhotoAlbumViewModel photoAlbumViewModel)
        {
            return new PhotoAlbumEntity
            {
                Id = photoAlbumViewModel.Id,
                Title = photoAlbumViewModel.Title,
                Description = photoAlbumViewModel.Description,
                Period = new DAL.DateTimePeriod(photoAlbumViewModel.Period),
                PlaceId = photoAlbumViewModel.PlaceId
            };
        }
    }
}
