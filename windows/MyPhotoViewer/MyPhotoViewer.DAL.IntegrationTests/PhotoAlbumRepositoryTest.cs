using MyPhotoViewer.DAL.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoViewer.DAL.IntegrationTests
{
    [TestFixture]
    public class PhotoAlbumRepositoryTest
    {
        [Test]
        public void CommonTest()
        {
            using (var photosContext = new PhotosContext())
            {
                photosContext.Database.Log = Console.WriteLine;

                var photoAlbums = photosContext.PhotoAlbums;
                Console.WriteLine("LALALALALALALA");

                var photoAlbum = photoAlbums.First();
                var photosCount = photoAlbum.Photos.Count();

                Console.WriteLine("Count: {0}", photosCount);
            }
        }
    }
}
