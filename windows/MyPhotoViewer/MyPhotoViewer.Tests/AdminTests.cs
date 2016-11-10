using Moq;
using MyPhotoViewer.Controllers;
using MyPhotoViewer.DAL;
using NUnit.Framework;
using System.Net;
using System.Web.Mvc;

namespace MyPhotoViewer.Tests
{
    [TestFixture]
    public class AdminTests
    {
        [Test]
        public void Can_Edit_Album()
        {
            int photoAlbumId = 1;
            Mock<IPhotoAlbumRepository> mockRepository = new Mock<IPhotoAlbumRepository>();
            mockRepository.Setup(mock => mock.GetPhotoAlbumById(photoAlbumId)).Returns(new PhotoAlbum { PhotoAlbumId = photoAlbumId });

            AdminController adminController = new AdminController(mockRepository.Object);
            PhotoAlbum photoAlbum = (adminController.Edit(photoAlbumId) as ViewResult)?.ViewData.Model as PhotoAlbum;

            Assert.IsNotNull(photoAlbum);
            Assert.AreEqual(photoAlbumId, photoAlbum.PhotoAlbumId);
        }

        [Test]
        public void Cannot_Edit_Nonexistent_Album()
        {
            int photoAlbumId = 1, nonExistentPhotoAlbumId = 2;
            Mock<IPhotoAlbumRepository> mockRepository = new Mock<IPhotoAlbumRepository>();
            mockRepository.Setup(mock => mock.GetPhotoAlbumById(photoAlbumId)).Returns(new PhotoAlbum { PhotoAlbumId = photoAlbumId });

            AdminController adminController = new AdminController(mockRepository.Object);
            int errorStatusCode = (adminController.Edit(nonExistentPhotoAlbumId) as HttpStatusCodeResult).StatusCode;

            Assert.IsTrue((int)HttpStatusCode.BadRequest == errorStatusCode);
        }

        [Test]
        public void Can_Save_Valid_Changes()
        {
            var mock = new Mock<IPhotoAlbumRepository>();
            var controller = new AdminController(mock.Object);
            var photoAlbum = new PhotoAlbum();

            ActionResult result = controller.Edit(photoAlbum);

            mock.Verify(m => m.SavePhotoAlbum(photoAlbum));
            Assert.IsNotInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Cannont_Save_Invalid_Changes()
        {
            var mock = new Mock<IPhotoAlbumRepository>();
            var controller = new AdminController(mock.Object);
            controller.ModelState.AddModelError("error", "error");

            ActionResult result = controller.Edit(new PhotoAlbum());

            mock.Verify(m => m.SavePhotoAlbum(It.IsAny<PhotoAlbum>()), Times.Never());
            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
