using MyPhotoViewer.Converters;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.ViewModels;

namespace MyPhotoViewer.Extensions
{
    public static class PhotoAlbumViewModelExtensions
    {
        public static PhotoAlbumEntity ToPhotoAlbum(this NewPhotoAlbumViewModel photoAlbumViewModel)
        {
            return PhotoAlbumCreator.ConvertToPhotoAlbum(photoAlbumViewModel);
        }
    }
}
