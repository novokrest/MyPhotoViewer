using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.ViewModels;


namespace MyPhotoViewer.Converters
{
    public class PhotoConverter
    {
        public static PhotoEntity ToPhotoEntity(PhotoViewModel photoViewModel)
        {
            return new PhotoEntity
            {
                Id = photoViewModel.PhotoId,
                PhotoAlbumId = photoViewModel.PhotoAlbumId,
                Title = photoViewModel.Title,
                Image = photoViewModel.Image.Data,
                ImageType = photoViewModel.Image.Type
            };
        }
    }
}