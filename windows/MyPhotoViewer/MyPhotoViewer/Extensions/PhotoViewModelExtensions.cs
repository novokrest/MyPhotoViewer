using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.ViewModels;

namespace MyPhotoViewer.Extensions
{
    public static class PhotoViewModelExtensions
    {
        public static PhotoEntity ToPhotoEntity(this NewPhotoViewModel newPhotoViewModel)
        {
            return new PhotoEntity
            {
                Title = newPhotoViewModel.Title,
                PhotoAlbumId = newPhotoViewModel.PhotoAlbumId,
                Image = newPhotoViewModel.Image.Data,
                ImageType = newPhotoViewModel.Image.Type
            };
        }
    }
}