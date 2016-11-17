using Moq;
using MyPhotoViewer.Controllers;
using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using MyPhotoViewer.ViewModels;
using NUnit.Framework;
using System.Web.Mvc;


namespace MyPhotoViewer.Tests
{
    [TestFixture]
    public class PhotoControllerTest
    {
        [Test]
        public void TestPhotoCreateGet()
        {
            const int testPhotoAlbumId = 1;
            var photoController = new PhotoController(CreatePhotoRepository());

            var viewResult = photoController.Create(testPhotoAlbumId) as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.IsEmpty(viewResult.ViewName);

            var photoViewModel = viewResult.ViewData.Model as PhotoViewModel;
            Assert.IsNotNull(photoViewModel);
            Assert.AreEqual(testPhotoAlbumId, photoViewModel.PhotoAlbumId);
        }

        [Test]
        public void TestPhotoCreatePost()
        {
            var photoController = new PhotoController(CreatePhotoRepository());
            var photoViewModel = new PhotoViewModel()
            {
                PhotoId = 1,
                PhotoAlbumId = 2,
                Title = "TestTitle",
                Image = new Image(new byte[0], ImageType.Jpeg)
            };

            var result = photoController.Create(photoViewModel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Album", result.RouteValues["controller"]);
            Assert.AreEqual(photoViewModel.PhotoAlbumId, result.RouteValues["photoAlbumId"]);
        }

        private static IPhotoRepository CreatePhotoRepository()
        {
            Mock<IPhotoRepository> mock = new Mock<IPhotoRepository>();

            return mock.Object;
        }
    }
}
