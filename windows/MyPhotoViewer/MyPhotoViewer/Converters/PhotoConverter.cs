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
                PhotoAlbumId = photoViewModel.AlbumId,
                Title = photoViewModel.Title,
                CreationDate = photoViewModel.CreationDate
            };
        }

        public static PhotoEntity ToPhotoEntity(NewPhotoViewModel photoViewModel)
        {
            var photoEntity = ToPhotoEntity((PhotoViewModel)photoViewModel);

            photoEntity.Image = photoViewModel.Image.Data;
            photoEntity.ImageType = photoViewModel.Image.Type;

            return photoEntity;
        }
    }
}