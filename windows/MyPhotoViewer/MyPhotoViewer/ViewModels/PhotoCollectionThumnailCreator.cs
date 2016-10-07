using MyPhotoViewer.DAL;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.ViewModels
{
    public class PhotoCollectionThumnailCreator
    {
        private readonly IPhotoCollectionRepository _photoCollectionRepository;

        public PhotoCollectionThumnailCreator(IPhotoCollectionRepository photoCollectionRepository)
        {
            _photoCollectionRepository = photoCollectionRepository;
        }

        public IReadOnlyList<IPhotoCollectionThumbnail> CreateThumbnails()
        {
            return CreateThumbnailsLazy().ToList();
        }

        private IEnumerable<IPhotoCollectionThumbnail> CreateThumbnailsLazy()
        {
            foreach(var photoCollection in LoadPhotoCollections())
            {
                yield return new PhotoCollectionThumbnail()
                {
                    PhotoCollectionId = photoCollection.PhotoCollectionId,
                    Name = photoCollection.Name,
                    Period = photoCollection.Period,
                    Place = photoCollection.Place,
                    Description = photoCollection.Description,
                    Image = photoCollection.Photos.First().PhotoId.ToString()
                };
            }
        }

        private IEnumerable<PhotoCollection> LoadPhotoCollections()
        {
            return _photoCollectionRepository.GetPhotoCollections();
        }
    }
}