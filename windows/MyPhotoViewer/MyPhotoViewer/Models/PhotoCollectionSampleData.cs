using MyPhotoViewer.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyPhotoViewer.Models
{
    public class PhotoCollectionSampleData : DropCreateDatabaseAlways<PhotoCollectionEntities>
    {
        protected override void Seed(PhotoCollectionEntities context)
        {
            var places = new List<Place>
            {
                new Place { Name = "Eiffel Tower", City = "Paris", Country = "France" },
                new Place { Name = "Hermitage", City = "Saint-Petersburg", Country = "Russia" },
                new Place { Name = "Corrida", City = "Barcelona", Country = "Spain" },
            };

            List<Photo> photos = new List<Photo>
            {
                new Photo { Title = "Photo1", CreationDate = DateTime.Now, Place = places.Single(place => place.Country == "France") },
                new Photo { Title = "Photo2", CreationDate = DateTime.Now, Place = places.Single(place => place.Country == "Spain") },
                new Photo { Title = "Photo3", CreationDate = DateTime.Now, Place = places.Single(place => place.Country == "Russia") },
                new Photo { Title = "Photo4", CreationDate = DateTime.Now, Place = places.Single(place => place.Country == "France") },
                new Photo { Title = "Photo5", CreationDate = DateTime.Now, Place = places.Single(place => place.Country == "Spain") },
                new Photo { Title = "Photo6", CreationDate = DateTime.Now, Place = places.Single(place => place.Country == "Russia") },
                new Photo { Title = "Photo7", CreationDate = DateTime.Now, Place = places.Single(place => place.Country == "France") }
            };

            var photoCollections = GetPhotoCollections();

            photoCollections.ForEach(photoCollection => context.PhotoCollections.Add(photoCollection));
            context.SaveChanges();
        }

        private static IReadOnlyCollection<PhotoCollection> GetPhotoCollections()
        {
            string photosRootDirectoryPath = System.Configuration.ConfigurationManager.AppSettings["PhotosRootDirectory"];
            var photosExplorer = new PhotosExplorer(photosRootDirectoryPath);

            return photosExplorer.GetPhotos();
        }
    }
}