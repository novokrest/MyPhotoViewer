using NUnit.Framework;
using System.Collections.Generic;

namespace MyPhotoViewer.DAL.IntegrationTests
{
    [TestFixture]
    public class PhotosContextTest
    {
        [Test]
        public void TestAddNewPhotoAlbumToDatabase()
        {
            var photoAlbum = new PhotoAlbum
            {
                Title = "TestPhotoAlbum",
                Description = "TestDescription",
                Period = new DateTimePeriod(),
                Place = new Place
                {
                    Name = "TestPlace",
                    City = "City",
                    Country = "Country"
                },
                Photos = new List<Photo>
                {
                    new Photo
                    {
                        Title = "TestPhoto#1",
                        Image = new byte[] { 1 },
                    },
                    new Photo
                    {
                        Title = "TestPhoto#2",
                        Image = new byte[] { 2 }
                    },
                }
            };

            using (var photosContext = new PhotosContext())
            {
                //photosContext.Configuration.AutoDetectChangesEnabled = false;
                //photosContext.Configuration.ValidateOnSaveEnabled = false;

                photosContext.PhotoAlbums.Add(photoAlbum);
                photosContext.SaveChanges();
            }
        }

        [Test]
        public void TestAddNewPhotoAlbumToDatabase2()
        {
            var place = new Place
            {
                Name = "TestPlace",
                City = "City",
                Country = "Country"
            };

            var photoAlbum = new PhotoAlbum
            {
                Title = "TestPhotoAlbum",
                Description = "TestDescription",
                Period = new DateTimePeriod(),
                Place = place,
                Photos = new List<Photo>
                {
                    new Photo
                    {
                        Title = "TestPhoto#1",
                        Image = new byte[] { 1 },
                        Place = place
                    },
                    new Photo
                    {
                        Title = "TestPhoto#2",
                        Image = new byte[] { 2 },
                        Place = place
                    },
                }
            };

            using (var photosContext = new PhotosContext())
            {
                //photosContext.Configuration.AutoDetectChangesEnabled = false;
                //photosContext.Configuration.ValidateOnSaveEnabled = false;

                photosContext.PhotoAlbums.Add(photoAlbum);
                photosContext.SaveChanges();
            }
        }
    }
}
