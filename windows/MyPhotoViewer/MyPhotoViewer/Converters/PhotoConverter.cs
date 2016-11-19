using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.ViewModels;


namespace MyPhotoViewer.Converters
{
    public class PhotoConverter
    {
        public static PhotoEntity ToPhotoEntity(BasePhotoViewModel photoViewModel)
        {
            return new PhotoEntity
            {
                Id = photoViewModel.PhotoId,
                PhotoAlbumId = photoViewModel.PhotoAlbumId,
                Title = photoViewModel.Title,
                CreationDate = photoViewModel.CreationDate
            };
        }

        public static PhotoEntity ToPhotoEntity(NewPhotoViewModel photoViewModel)
        {
            var photoEntity = ToPhotoEntity((BasePhotoViewModel)photoViewModel);

            photoEntity.Image = photoViewModel.Image.Data;
            photoEntity.ImageType = photoViewModel.Image.Type;

            return photoEntity;
        }
    }
}