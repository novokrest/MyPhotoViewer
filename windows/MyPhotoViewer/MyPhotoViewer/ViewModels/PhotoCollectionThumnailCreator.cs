using MyPhotoViewer.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.ViewModels
{
    public class PhotoCollectionThumnailCreator
    {
        private readonly IPhotoCollectionEnitites _photosDb;

        public PhotoCollectionThumnailCreator(IPhotoCollectionEnitites photosDb)
        {
            _photosDb = photosDb;
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
                    Name = photoCollection.Name,
                    Period = photoCollection.Period,
                    Place = photoCollection.Place,
                    Description = photoCollection.Description,
                    Image = photoCollection.Photos[0].Id.ToString()
                };
            }
        }

        private IReadOnlyCollection<PhotoCollection> LoadPhotoCollections()
        {
            return _photosDb.PhotoCollections.Include("Photos").Include("Place").ToList();
        }
    }
}