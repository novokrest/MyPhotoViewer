using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MyPhotoViewer.DAL.IntegrationTests
{
    [TestFixture]
    public class PhotosContextTest
    {
        [Test]
        public void TestPhotosExist()
        {
            using (var photosContext = new PhotosDbContext())
            {
                int photosCount = photosContext.Photos.Count();
                Assert.NotZero(photosCount);
            }
        }

        [Test]
        public void TestAddNewPhotoAlbumToDatabase()
        {
            var photoAlbum = new PhotoAlbumEntity
            {
                Title = "TestPhotoAlbum",
                Description = "TestDescription",
                Period = new DateTimePeriod(),
                Place = new PlaceEntity
                {
                    Name = "TestPlace",
                    City = "City",
                    Country = "Country"
                },
                Photos = new List<PhotoEntity>
                {
                    new PhotoEntity
                    {
                        Title = "TestPhoto#1",
                        Image = new byte[] { 1 },
                    },
                    new PhotoEntity
                    {
                        Title = "TestPhoto#2",
                        Image = new byte[] { 2 }
                    },
                }
            };

            using (var photosContext = new PhotosDbContext())
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
            var place = new PlaceEntity
            {
                Name = "TestPlace",
                City = "City",
                Country = "Country"
            };

            var photoAlbum = new PhotoAlbumEntity
            {
                Title = "TestPhotoAlbum",
                Description = "TestDescription",
                Period = new DateTimePeriod(),
                Place = place,
                Photos = new List<PhotoEntity>
                {
                    new PhotoEntity
                    {
                        Title = "TestPhoto#1",
                        Image = new byte[] { 1 },
                        Place = place
                    },
                    new PhotoEntity
                    {
                        Title = "TestPhoto#2",
                        Image = new byte[] { 2 },
                        Place = place
                    },
                }
            };

            using (var photosContext = new PhotosDbContext())
            {
                //photosContext.Configuration.AutoDetectChangesEnabled = false;
                //photosContext.Configuration.ValidateOnSaveEnabled = false;

                photosContext.PhotoAlbums.Add(photoAlbum);
                photosContext.SaveChanges();
            }
        }
    }
}
