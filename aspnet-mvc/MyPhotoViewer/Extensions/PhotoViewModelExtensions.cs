using MyPhotoViewer.Converters;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.ViewModels;

namespace MyPhotoViewer.Extensions
{
    public static class PhotoViewModelExtensions
    {
        public static PhotoEntity ToPhotoEntity(this PhotoViewModel photoViewModel)
        {
            return PhotoConverter.ToPhotoEntity(photoViewModel);
        }

        public static PhotoEntity ToPhotoEntity(this NewPhotoViewModel photoViewModel)
        {
            return PhotoConverter.ToPhotoEntity(photoViewModel);
        }
    }
}