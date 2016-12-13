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
            var photoController = CreatePhotoController();

            var viewResult = photoController.Create(testPhotoAlbumId) as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.IsEmpty(viewResult.ViewName);

            var photoViewModel = viewResult.ViewData.Model as NewPhotoViewModel;
            Assert.IsNotNull(photoViewModel);
            Assert.AreEqual(testPhotoAlbumId, photoViewModel.AlbumId);
        }

        [Test]
        public void TestPhotoCreatePost()
        {
            var photoController = CreatePhotoController();
            var photoViewModel = CreateFilledPhotoViewModel();

            var result = photoController.Create(photoViewModel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Album", result.RouteValues["controller"]);
            Assert.AreEqual(photoViewModel.AlbumId, result.RouteValues["photoAlbumId"]);
        }

        [Test]
        public void TestPhotoEditGet()
        {
            var photoController = CreatePhotoController();
            var photoViewModel = CreateFilledPhotoViewModel();

            var result = photoController.Edit(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsEmpty(result.ViewName);
        }

        [Test]
        public void TestPhotoEditPost()
        {

        }

        private static PhotoController CreatePhotoController()
        {
            return new PhotoController(CreatePhotoRepository(), CreatePhotoAlbumRepository());
        }

        private static IPhotoRepository CreatePhotoRepository()
        {
            Mock<IPhotoRepository> mock = new Mock<IPhotoRepository>();
            Mock<IPhoto> photo = new Mock<IPhoto>();

            mock.Setup(m => m.GetPhotoById(1)).Returns(photo.Object);

            return mock.Object;
        }

        private static IAlbumRepository CreatePhotoAlbumRepository()
        {
            return new Mock<IAlbumRepository>().Object;
        }

        private static NewPhotoViewModel CreateFilledPhotoViewModel()
        {
            return new NewPhotoViewModel()
            {
                PhotoId = 1,
                AlbumId = 2,
                Title = "TestTitle",
                Image = new Image(new byte[0], ImageType.Jpeg)
            };
        }
    }
}
